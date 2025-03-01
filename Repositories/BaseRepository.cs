using Microsoft.EntityFrameworkCore;
using SmartScheduler.Data;
using SmartScheduler.Data.Models;
using SmartScheduler.Exceptions;
using SmartScheduler.Repositories.Contracts;
using System.Net;

namespace SmartScheduler.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected BaseRepository(AppDbContext context) {
            Context = context;
            EntityTable = context.Set<TEntity>();
        }

        protected AppDbContext Context { get; }
        protected DbSet<TEntity> EntityTable { get; }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            TEntity addedEntity = (await EntityTable.AddAsync(entity)).Entity;

            try
            {
                _ = await Context.SaveChangesAsync();
                return addedEntity;
            }
            catch (Exception)
            { 
                throw new ClientException($"Could not create {typeof(TEntity).FullName}", HttpStatusCode.InternalServerError);
            }
        }

        public virtual async Task DeleteByIdAsync(int entityId)
        {
            TEntity? entityToDelete = await EntityTable.FirstOrDefaultAsync(e => e.Id == entityId);

            if (entityToDelete == null) {
                throw new ClientException($"Could not delete {typeof(TEntity).FullName} with id {entityId} because it doesn't exist", HttpStatusCode.NotFound);
            }

            EntityTable.Remove(entityToDelete);
            _ = await Context.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await EntityTable.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int entityId)
        {
            TEntity? entity = await EntityTable.FirstOrDefaultAsync(e => e.Id == entityId);

            return entity ?? throw new ClientException($"Could not find {typeof(TEntity).FullName} with id {entityId}", HttpStatusCode.NotFound);
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null || entity.Id <= 0)
            {
                throw new ClientException("Cannot update entity", HttpStatusCode.BadRequest);
            }

            try
            {
                var existingEntity = await EntityTable.FirstOrDefaultAsync(e => e.Id == entity.Id);

                if (existingEntity == null)
                {
                    throw new ClientException($"Entity with ID {entity.Id} not found", HttpStatusCode.NotFound);
                }

                // Update the existing entity's properties with the values from the provided entity
                Context.Entry(existingEntity).CurrentValues.SetValues(entity);

                // Mark the existing entity as modified so that EF Core knows to update it
                Context.Entry(existingEntity).State = EntityState.Modified;

                await Context.SaveChangesAsync();

                return existingEntity;
            }
            catch (DbUpdateException)
            {
                throw new ClientException("Database update failed", HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                throw new ClientException("Could not update entity", HttpStatusCode.InternalServerError);
            }
        }
    }
}

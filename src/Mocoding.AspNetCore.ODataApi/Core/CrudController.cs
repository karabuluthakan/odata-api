﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Mocoding.AspNetCore.ODataApi.DataAccess;

namespace Mocoding.AspNetCore.ODataApi.Core
{
    public class CrudController<TEntity> : ODataController
        where TEntity : class, IEntity, new()
    {
        public CrudController(ICrudRepository<TEntity> repository)
        {
            Repository = repository;
        }

        [ActionContext]
        public ActionContext ActionContext { get; set; }

        protected ICrudRepository<TEntity> Repository { get; }

        [EnableQuery]
        public IQueryable<TEntity> Get()
        {
            return Repository.QueryRecords();
        }

        public virtual TEntity Get([FromODataUri] Guid key)
        {
            var entity = Repository.QueryRecords().FirstOrDefault(_ => _.Id == key);
            if (entity == null)
                throw new ArgumentNullException();

            return entity;
        }

        public virtual async Task<TEntity> Post([FromBody]TEntity entity)
        {
            return await Repository.AddOrUpdate(entity);
        }

        public virtual async Task<TEntity> Put(Guid key, [FromBody]TEntity entity)
        {
            entity.Id = key;
            return await Repository.AddOrUpdate(entity);
        }

        public virtual async Task<TEntity> Patch(Guid key, [FromBody]Delta<TEntity> moviePatch)
        {
            var entity = Repository.QueryRecords().FirstOrDefault(_ => _.Id == key);
            if (entity == null)
                throw new ArgumentNullException();

            moviePatch.CopyChangedValues(entity);
            return await Repository.AddOrUpdate(entity);
        }

        public virtual async Task Delete(Guid key)
        {
            await Repository.Delete(key);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    // Generic repository that works with all kinds of classes. This is the one that should be used for common methods.
    // Also known as the template pattern
    public class Repository<T> : IRepository<T> where T : class
    {
        // The database context for the entire class
        protected readonly BarOMeterContext _dbContext;

        /// <summary>
        /// Takes the database context and sets it for the repository class, so it's the same throughout the
        /// calls of the class.
        /// </summary>
        /// <param name="dbcontext">
        /// Takes the database context and sets it in the class.
        /// </param>
        public Repository(BarOMeterContext dbcontext)
        {
            _dbContext = dbcontext;
        }


        /// <summary>
        /// Adds an object of a given type to the database, if this type is valid inside the database.
        /// </summary>
        /// <param name="entity">
        /// Takes a class entity and adds it to the repository if it's an acceptable class.
        /// </param>
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }


        /// <summary>
        /// Takes the keys for a given object and deletes it from the database.
        /// </summary>
        /// <param name="keys">
        /// If using a composite key, then it has to be in matching order of how it's defined in the Fluent API.
        /// </param>
        public void Delete(params object [] keys)
        {
            _dbContext.Set<T>().Remove(Get(keys));
        }


        /// <summary>
        /// Takes an expression as parameter and returns list of the matching objects.
        /// </summary>
        /// <param name="predicate"></param>
        /// Can for example take x=>x.Bar.BarName == "BarName";
        /// <returns>
        /// If successful, returns list of the matching object if existing.
        /// If unsuccessful, null.
        /// </returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AsEnumerable().ToList();
        }


        /// <summary>
        /// Takes the id of a type and returns the object from database.
        /// </summary>
        /// <param name="keys">
        /// Can take composite keys by typing Get(key1, key2, key3...).
        /// Have to be in exact order defined in fluent api. Drinks e.g. has to be Get(barname, drinkname).
        /// </param>
        /// <returns>
        /// If successful, object if it exists and param was correct.
        /// If unsuccessful, null.
        /// </returns>
        public T Get(params object[] keys)
        {
            return _dbContext.Set<T>().Find(keys);
        }


        /// <summary>
        /// Takes no parameters and returns a list of all the objects of the given type.
        /// </summary>
        /// <returns>
        /// If successful, list(as IEnumerable) of objects of the given type if they exist.
        /// If unsuccessful, null.
        /// </returns>
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().AsEnumerable().ToList();
        }
    }
}

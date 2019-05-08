using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

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
        T Get(params object [] keys);

        /// <summary>
        /// Takes no parameters and returns a list of all the objects of the given type.
        /// </summary>
        /// <returns>
        /// If successful, list(as IEnumerable) of objects of the given type if they exist.
        /// If unsuccessful, null.
        /// </returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Takes an expression as parameter and returns list of the matching objects.
        /// </summary>
        /// <param name="predicate">
        /// Can for example take x=>x.Bar.BarName == "BarName";
        /// </param>
        /// <returns>
        /// If successful, returns list of the matching object if existing.
        /// If unsuccessful, null.
        /// </returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds an object of a given type to the database, if this type is valid inside the database.
        /// </summary>
        /// <param name="entity">
        /// Takes a class entity and adds it to the repository if it's an acceptable class.
        /// </param>
        void Add(T entity);

        /// <summary>
        /// Takes the keys for a given object and deletes it from the database.
        /// </summary>
        /// <param name="keys">
        /// If using a composite key, then it has to be in matching order of how it's defined in the Fluent API.
        /// </param>
        void Delete(params object [] keys);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLine
{
#warning  Not used now...
    public interface IAirlineEntity
    {

    }

    public interface ISearchCriteria<T> where T: IAirlineEntity
    {

    }
    public interface IAirlineEntityContainer<T> where T : IAirlineEntity
    {

        void Add(T item);
        void Delete(T item);
        /// <summary>
        /// Not sure if I need it, because editing it means find exact object and just change properties
        /// </summary>
        /// <param name="item"></param>
        //void Update(T item);
        // possible I will need Search one element only
        // T Search(ISearchCriteria<T> searchCriteria);
        IEnumerable<T> Search(ISearchCriteria<T> searchCriteria);
    }


}

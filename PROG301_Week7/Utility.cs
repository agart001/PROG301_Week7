using PROG301_Week7.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG301_Week7
{
    public static class Utility
    {
        public static O CreateInstance<O>(Type t) => (O)Activator.CreateInstance(t.Assembly.FullName, t.FullName).Unwrap();

        public static ObservableCollection<O> IEnumToObsCol<O>(IEnumerable<O> collection) => new ObservableCollection<O>(collection);

        public static List<O> ObsColToList<O>(ObservableCollection<O> o_collection) => o_collection as List<O>;
    }
}

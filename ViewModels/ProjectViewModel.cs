using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bug_Tracker.Models;

namespace Bug_Tracker.ViewModels
{
    public class ProjectViewModel
    {
        public int projektID { get; set; }
        public string opisProjekta { get; set; }
        public string nazivProjekta { get; set; }
        public string datumPokretanja { get; set; }

        public int brojListica { get; set; }

        public ProjectViewModel()
        {
            brojListica = 0;
        }

    }
}
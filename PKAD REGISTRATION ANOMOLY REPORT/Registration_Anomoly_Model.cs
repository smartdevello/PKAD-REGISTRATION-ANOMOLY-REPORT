using System;
using System.Collections.Generic;
using System.Text;

namespace PKAD_REGISTRATION_ANOMOLY_REPORT
{
    public class Registration_Anomoly_Model
    {
        public string snapshot_dt { get; set; }
        public int year;
        public int month;
        public int day;
        public int precinct { get; set; }
        public string precinct_name { get; set; }
        public int additions { get; set; }
        public int additions_voted { get; set; }
        public int removals { get; set; }
        public int removals_voted { get; set; }
        public int md_ghosts_voted_removed { get; set; }
        public int registered { get; set; }
        public int voted { get; set; }
    }
}

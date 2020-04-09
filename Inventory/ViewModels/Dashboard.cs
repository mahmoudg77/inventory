using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class Dashboard
    {
        public int assets_count { get; set; }
        public int users_count { get; set; }
        public int requests_app_count { get; set; }
        public int requests_pen_count { get; set; }
        public int requests_rej_count { get; set; }
        public int ast_pc_count { get; set; }
        public int ast_ip_count { get; set; }
        public int ast_kp_count { get; set; }
        public int ast_lp_count { get; set; }
        public int ast_ma_count { get; set; }
        public int ast_tp_count { get; set; }

    }
}
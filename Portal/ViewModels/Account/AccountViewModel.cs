using Portal.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.ViewModels.Account
{
    public class AccountViewModel
    {
        public static List<SelectListItem> GetAllRoles(int roleId)
        {
            List<SelectListItem> roles = new List<SelectListItem>();
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using(SqlCommand cmd = new SqlCommand("usp_RolesGetRolesByRoleId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    while(reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["RoleName"].ToString();
                        item.Text = reader["RoleName"].ToString();

                        roles.Add(item);

                    }
                }

            }




                return roles;

        }
    }
}
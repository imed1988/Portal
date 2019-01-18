using Portal.Models.Account;
using Portal.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

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

        public static UserProfileModel GetUserProfileData(int currentUserId)
        {
            UserProfileModel userProfileModel = new UserProfileModel();

            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UsersGetUserProfileData", conn))
                        {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", currentUserId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    userProfileModel.FullName = reader["FullName"].ToString();
                    userProfileModel.Mail = reader["Mail"].ToString();
                    userProfileModel.Address = reader["Address"].ToString();
                }
            }

            return userProfileModel;
        }

        public static void UpdateUserProfile(UserProfileModel upm)
        {
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UsersUpdateUserProfiles", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserId", WebSecurity.CurrentUserId);
                    cmd.Parameters.AddWithValue("@FullName", upm.FullName);
                    cmd.Parameters.AddWithValue("@Mail", upm.Mail);
                    cmd.Parameters.AddWithValue("@Address", upm.Address);

                     cmd.ExecuteNonQuery();

                }
            }
        }

        public static List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UsersGetAllUsers", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        UserModel user = new UserModel();
                        user.UserId = Convert.ToInt32(reader["UserId"]);
                        user.UserName = reader["UserName"].ToString();
                        user.FullName = reader["FullName"].ToString();
                        user.Mail = reader["Mail"].ToString();

                        users.Add(user);
                    }


                }
            }

            return users;
        }





    }
}
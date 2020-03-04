using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CustomWebApp.Models {
    public class User {
        public bool LoggedInStatus { get; set; } = false;
        public string Username { get; set; }
        private int UserId { get; set; }

        private User Manual_Validate_Login(string user, string pass) { //example to show I know about parameterized queries and how to query a db
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("SELECT ID, USERNAME FROM USER WHERE USERNAME = @Username AND PASSWORD = @Password", con)) {
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass);
                    try {
                        con.Open();
                        using(var reader = cmd.ExecuteReader()) {
                            while(reader.Read()) {
                                for(int i = 0; i < reader.FieldCount; i++)
                                    switch(reader.GetName(i).ToLower()) {
                                        case "id":
                                            UserId = reader.GetInt32(i);
                                            break;
                                        case "name":
                                            Username = reader.GetString(i);
                                            break;
                                    }
                            }
                        }
                        con.Close();
                    } catch {
                        UserId = -3;
                    }
                    return this;
                }
            }
        }
        public User Validate_Login(string user, string pass) {
            Username = user; // can set it here and check later, not the best plan
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("SProc_User_Login", con) { CommandType = CommandType.StoredProcedure }) { //select
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        UserId = Convert.ToInt32(cmd.ExecuteScalar()); //could Int32.TryParse if stored as string
                        con.Close();
                    } catch {
                        UserId = -3;
                    }
                    return this;
                }
            }
        }
        public (bool isValid, int rowsAffected) Manual_Register(string user, string pass) {
            var rowsAffected = int.MinValue;
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("INSERT INTO USER (USERNAME, PASSWORD) VALUES (@Username, @Password)", con)) { //insert
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    } catch {
                        rowsAffected = int.MinValue;
                    }
                    return (isValid: rowsAffected == 1, rowsAffected: rowsAffected);
                }
            }
        }
        public (bool isValid, int rowsAffected) Register(string user, string pass) {
            var rowsAffected = int.MinValue;
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("SProc_Register_Login", con) { CommandType = CommandType.StoredProcedure }) { //insert
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    } catch {
                        rowsAffected = int.MinValue;
                    }
                    return (isValid: rowsAffected == 1, rowsAffected: rowsAffected);
                }
            }
        }
        public (bool isValid, int rowsAffected) Manual_UpdateUsername(string user, string pass) {
            var rowsAffected = int.MinValue;
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("UPDATE USER SET USER.USERNAME = @Username WHERE USER.USERNAME = @OrigUsername AND USER.PASSWORD = @Password", con)) { //update
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@OrigUsername", Username);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    } catch {
                        rowsAffected = int.MinValue;
                    }
                    return (isValid: rowsAffected == 1, rowsAffected: rowsAffected);
                }
            }
        }
        public (bool isValid, int rowsAffected) UpdateUsername(string user, string pass) {
            var rowsAffected = int.MinValue;
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("SProc_Update_Login_User", con) { CommandType = CommandType.StoredProcedure }) { //update
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    } catch {
                        rowsAffected = int.MinValue;
                    }
                    return (isValid: rowsAffected == 1, rowsAffected: rowsAffected);
                }
            }
        }
        public (bool isValid, int rowsAffected) Manual_UpdatePassword(string user, string pass) {
            var rowsAffected = int.MinValue;
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("UPDATE USER SET USER.PASSWORD = @Password WHERE USER.USERNAME = @Username", con)) { //update
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    } catch {
                        rowsAffected = int.MinValue;
                    }
                    return (isValid: rowsAffected == 1, rowsAffected: rowsAffected);
                }
            }
        }
        public (bool isValid, int rowsAffected) UpdatePassword(string user, string pass) {
            var rowsAffected = int.MinValue;
            using(var con = new SqlConnection(ConnectionManager.Default)) {
                using(var cmd = new SqlCommand("SProc_Update_Login_Pass", con) { CommandType = CommandType.StoredProcedure }) { //update
                    cmd.Parameters.AddWithValue("@Username", user);
                    cmd.Parameters.AddWithValue("@Password", pass); //could hash pass here and check for the same hash
                    try {
                        con.Open();
                        rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();
                    } catch {
                        rowsAffected = int.MinValue;
                    }
                    return (isValid: rowsAffected == 1, rowsAffected: rowsAffected);
                }
            }
        }
        public (User, String) Tuple_Login(string user, string pass) {
            return (Validate_Login(user, pass), this.LoginResult);
        }
        public string LoginResult {
            get {
                try {
                    if(LoggedInStatus) LoggedInStatus = false;
                    switch(UserId) {
                        case -1:
                            return "Invalid Username and/or password";
                        case -2:
                            return "Invalid Attempt, account not activated.";
                        case -3:
                            return "Invalid Attempt, maintenance of the database";
                        case int user when user > 0:
                            LoggedInStatus = true;
                            return $"Sucessfully logged in as {Username} (id: {UserId})";
                        default:
                            return null;
                    }
                } finally {
                    //if(!LoggedInStatus) Username = null;
                }
            }
        }
    }
}

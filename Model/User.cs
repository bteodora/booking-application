﻿using BookingApp.Serializer;
using System;

namespace BookingApp.Model
{
    public enum UserType{ host, tourist, guest, guide }

    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserType UserType { get; set; }

        public User() { }

        public User(string username, string password, UserType type)
        {
            Username = username;
            Password = password;
            UserType = type;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, UserType.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            if (values[3].Equals("host"))
            { 
                UserType = UserType.host;
            }
            else if (values[3].Equals("tourist")){
                UserType = UserType.tourist;
            }
            else if (values[3].Equals("guest")){
                UserType = UserType.guest;
            }
            else if (values[3].Equals("guide")){
                UserType = UserType.guide;
            }
        }
    }
}

﻿namespace Academy.Domain.AppSettingModels
{
    public class SuperAdmin
    {
        public required string Username { get; set; }
        public required string FullName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}

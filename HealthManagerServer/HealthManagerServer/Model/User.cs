﻿namespace HealthManagerServer.Model;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public double Weight { get; set; }
    public Gender Gender { get; set; }
}
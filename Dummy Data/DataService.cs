using System;
using System.Collections.Generic;
using Contracts;

namespace Dummy_Data
{
    public class DataService
    {

        public List<Timestamp> GetDummyTimestamps()
        {
            var dummyTimestamps = new List<Timestamp>();

            var dummyTimestamp = new Timestamp
            {
                Id = 1,
                Description = "Hejsan",
                InternDescription = "Hallå Interndescription",
                Wage = true,
                IsDebiting = true,
                DefaultActivity = true,
                Order = new Order
                {

                },
                Employee = new Employee
                {
                    Adress = "Bromsgatan 6b",
                    City = "Örebro",
                    Email = "Marcus.Carlsson@itmastaren.se",
                    EmployeeId = 2,
                    Name = "Marcus Carlsson",

                   
                },
                Activity = new Activity
                {
                    Id = "1abc",
                    Description = "Spam"
                },
                Service = new Service
                {
                    Id = "1abc",
                    Description = "Visma Tidsinstämpling"
                },
                Customer = new Customer(),

                Agreement = new Agreement
                {
                    Id = 1,
                    Name = "Guld Spam Skydd"
                },
                Project = new Project
                {
                    Id = "Nemo Project"
                },
                DateFrom = new DateTime(2016, 04, 25),
                DateTo = new DateTime(2016, 04, 25),
                Occupation = new WorkText
                {
                    Id = 1,
                    GroupId = 48,
                    Text = "Awesome dude"
                }
            };

               var dummyTimestamp2 = new Timestamp
            {
                Id = 1,
                Description = "Awesome",
                InternDescription = "Hallå där interntext",
                //Tax = 1,
                Wage = true,
                IsDebiting = true,
                DefaultActivity = true,
                //OutLay = true,
                Order = new Order
                {

                },
                Employee = new Employee
                {
                    Adress = "Bromsgatan 6b",
                    City = "Örebro",
                    Email = "Rickard.Kamel@itmastaren.se",
                    EmployeeId = 2,
                    Name = "Rickard Kamel",

                   
                },
                Activity = new Activity
                {
                    Id = "2abc",
                    Description = "Brandvägg"
                },
                Service = new Service
                {
                    Id = "2abc",
                    Description = "ITM App"
                },
                Customer = new Customer(),

                Agreement = new Agreement
                {
                    Id = 1,
                    Name = "Silver Spam Skydd"
                },
                Project = new Project
                {
                    Id = "abc1",
                    Name = "Awesome Project"
                },
                DateFrom = new DateTime(2015, 03, 24),
                DateTo = new DateTime(2015, 03, 24),
                Occupation = new WorkText
                {
                    Id = 2,
                    GroupId = 48,
                    Text = "dude"
                }
            };

            dummyTimestamps.Add(dummyTimestamp);
            dummyTimestamps.Add(dummyTimestamp2);

            return dummyTimestamps;
        }
    }
}
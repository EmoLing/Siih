using Bogus;
using Core.Models.Departments;
using Core.Models.Equipment;
using Core.Models.Users;
using Infrastructure;

return;
//using var db = new ApplicationDBContext(null);

//var fakerUser = new Faker<User>("ru")
//    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
//    .RuleFor(u => u.LastName, f => f.Name.LastName())
//    .RuleFor(u => u.Surname, f => f.Name.Prefix())
//    .RuleFor(u => u.Name, f => f.Name.FullName());

//var fakerDepartment = new Faker<Department>("ru")
//    .RuleFor(d => d.Name, f => f.Commerce.Department());

//var fakerJobTitle = new Faker<JobTitle>("ru")
//    .RuleFor(jb => jb.Name, f => f.Name.JobTitle());

//var fakerSoftware = new Faker<Software>()
//    .RuleFor(s => s.Version, f => f.System.Version().ToString())
//    .RuleFor(s => s.Name, f => f.Commerce.ProductName());

//var fakerHardware = new Faker<Hardware>()
//    .RuleFor(h => h.Name, h => h.Commerce.ProductName())
//    .RuleFor(h => h.SerialNumber, f => f.Random.AlphaNumeric(10).ToUpper()).RuleFor(h => h.Article, f => $"ART-{f.Random.Number(1000, 9999)}")
//    .RuleFor(h => h.DateCreate, f => f.Date.PastDateOnly(5));

//var users = fakerUser.Generate(20);
//var jobTitles = fakerJobTitle.Generate(5);
//var departments = fakerDepartment.Generate(3);
//var softwares = fakerSoftware.Generate(20);
//var hardwares = fakerHardware.Generate(20);

//db.Users.AddRange(users);
//db.JobTitles.AddRange(jobTitles);
//db.Departments.AddRange(departments);
//db.Softwares.AddRange(softwares);
//db.Hardwares.AddRange(hardwares);

//db.SaveChanges();

//Console.WriteLine("Готово!\r\nНажмите на любую кнопку!");
//Console.ReadLine();
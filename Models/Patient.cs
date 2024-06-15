namespace FisioSolution.Models;

public class Patient
{
   public int PatientId { get; set; }
   public string? Name { get; set; }
   public string? Dni { get; set; }
   public string? Password { get; set; }

   private DateTime birthDate;
   public DateTime BirthDate
   {
      get => birthDate.Date;
      set => birthDate = value.Date;
   }

   public decimal Weight { get; set; }
   public decimal Height { get; set; }
   public bool Insurance { get; set; }
   public static int PatientIdSeed { get; set; }

   public Patient() {
      
   }

   public Patient(string name, string dni, string password, DateTime birthDate, decimal weight, decimal height, bool insurance) 
   {
      PatientId = PatientIdSeed++;
      Name = name;
      Dni = dni;
      Password = password;
      BirthDate = birthDate.Date;
      Weight = weight;
      Height = height;
      Insurance = insurance;
   }
}
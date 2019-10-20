using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseInitializer: DropCreateDatabaseIfModelChanges<Contexto>
    {
        protected override void Seed(Contexto context)
        {
            var Paciente = new List<Pacientes>
            {
               new Pacientes
                {
                   Nombres="Juan Perez"
                },
                new Pacientes
                {
                    Nombres="Maria Santana"
                },
                new Pacientes
                {
                    Nombres="Carlos ROdriguez"
                },
                new Pacientes
                {
                    Nombres="Yessica Valdez"
                }
            };
            Paciente.ForEach(s => context.Paciente.Add(s));
            context.SaveChanges();
        }
    }
}
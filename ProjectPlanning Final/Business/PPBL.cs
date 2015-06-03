using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class PPBL
    {
        PPDAO PP = new PPDAO();
        public string GetProjName(string dbProjCode)
        {
            return PP.GetProjName(dbProjCode);
        }

        public string GetProjStart(string dbProjCode)
        {
            return PP.GetProjStart(dbProjCode);
        }

        public string GetProjEnd(string dbProjCode)
        {
            return PP.GetProjEnd(dbProjCode);
        }

        public string GetProjID(string dbProjCode)
        {
            return PP.GetProjID(dbProjCode);
        }

        public List<string> GetProjResources(string dbProjID)
        {
            return PP.GetProjResources(dbProjID);
        }

        public List<string> GetProjOpenResources(string clickDate)
        {
            return PP.GetProjOpenResources(clickDate);
        }

        public static void InsertProject(string name, string code, string start, string end)
        {
            PPDAO.InsertProject(name, code,  start,  end);
        }

        public static void InsertAssignments(string name, string code, string resources)
        {
            PPDAO.InsertAssignments(name, code, resources);
        }

        public string GetProjDate(string dbProjCode, string pos)
        {
            return PP.GetProjDate(dbProjCode, pos);
        }

        public static void UpdateProject(string oldCode, string name, string code, string start, string end)
        {
            PPDAO.UpdateProject(oldCode, name, code, start, end);
        }

        public static void UpdateAssignments(string name, string code, string resources)
        {
            PPDAO.UpdateAssignments(name, code, resources);
        }
    }
}

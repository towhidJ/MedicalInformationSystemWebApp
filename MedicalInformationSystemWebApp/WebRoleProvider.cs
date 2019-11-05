using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MedicalInformationSystemWebApp.Models.CodeFirstModel;

namespace MedicalInformationSystemWebApp
{
    public class WebRoleProvider : RoleProvider
    {
        //public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } //VS-2019
        public override string ApplicationName { get; set; } //vs-2013

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var context = new MedicalInfoSys())
            {
                if (context.AdminTBs.Any(x=>x.Email==username))
                {
                    var AdminResult = (from roleTB in context.RoleTBs
                        join adminTB in context.AdminTBs on roleTB.Id equals adminTB.RoleId
                        where adminTB.Email == username
                        select roleTB.Role).ToArray();
                    return AdminResult;
                }
                if (context.DoctorTBs.Any(x => x.Email == username))
                {
                    var DoctorResult = (from roleTB in context.RoleTBs
                        join doctorTB in context.DoctorTBs on roleTB.Id equals doctorTB.RoleId
                        where doctorTB.Email == username
                        select roleTB.Role).ToArray();
                    return DoctorResult;
                }
                if (context.NurseTBs.Any(x => x.Email == username))
                {
                    var NurseResult = (from roleTB in context.RoleTBs
                        join nurseTB in context.NurseTBs on roleTB.Id equals nurseTB.RoleId
                        where nurseTB.Email == username
                        select roleTB.Role).ToArray();
                    return NurseResult;
                }
                if (context.ReceptionTBs.Any(x => x.Email == username))
                {
                    var ReceptionResult = (from roleTB in context.RoleTBs
                        join receptionTB in context.ReceptionTBs on roleTB.Id equals receptionTB.RoleId
                        where receptionTB.Email == username
                        select roleTB.Role).ToArray();
                    return ReceptionResult;
                }


            }
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
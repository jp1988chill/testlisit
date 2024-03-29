﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Prueba.Domain.Models
{
    public class ActualizarUserModel
    {
        //Lógica Microservicio...
        public bool ActualizarUsers(List<User> users, IRepositoryEntityFrameworkCQRS<User> userRepository){
            foreach (User user in users) {
                User UserToBeUpdated = userRepository.GetAll().Where(id => id.Name == user.Name).FirstOrDefault();
                if (UserToBeUpdated != null) {
                    UserToBeUpdated.Idroluser = user.Idroluser;
                    UserToBeUpdated.Name = user.Name;
                    UserToBeUpdated.Password = user.Password;
                    UserToBeUpdated.Token = user.Token;
                    UserToBeUpdated.Tokenleasetime = user.Tokenleasetime;
                    userRepository.Update(UserToBeUpdated);
                }
            }
            if (userRepository.Save() > 0) {
                return true;
            }
            return false;
        }
        public async Task<UserResponse> ActualizarUser(UserBody objBodyObjectRequest, IRepositoryEntityFrameworkCQRS<User> userRepository)
        {
            int httpCod = 200;
            string httpMsg = "Registros Procesados Correctamente";
            string moreInfo = "200 - Success";
            string usrFriendlyErr = "Registros Procesados Correctamente";

            if (ActualizarUsers(objBodyObjectRequest.Users, userRepository) != true) {
                httpCod = 400;
                httpMsg = "Error al ingresar usuarios";
                moreInfo = httpCod + " - Error";
                usrFriendlyErr = httpMsg;
            }

            UserResponse bodyResponse = new UserResponse()
            {
                HttpCode = httpCod,
                HttpMessage = httpMsg,
                MoreInformation = moreInfo,
                userFriendlyError = usrFriendlyErr,
                usersNuevoTokenAsignado = objBodyObjectRequest.Users
            };
            await Task.CompletedTask.ConfigureAwait(false);
            return bodyResponse;
        }
    }
}

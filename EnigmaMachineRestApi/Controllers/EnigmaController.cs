using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EnigmaMachineRestApi.Models;
using Newtonsoft.Json;
using Enigma;

namespace EnigmaMachineRestApi.Controllers
{
    [RoutePrefix("api/Enigma")]
    public class EnigmaController : ApiController
    {
        public static EnigmaMachine enigmaMachine;

        [Route("SetRotor/{id:int:Range(1,3)}")]
        [HttpPost]
        public HttpResponseMessage SetRotor([FromBody] RotorDto rotorDto, [FromUri] int id)
        {
            HttpResponseMessage response;

            string json = JsonConvert.SerializeObject(rotorDto, Formatting.Indented);

            try
            {
                enigmaMachine.SetRotorDial(id, rotorDto.InitialDialSetting);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }

            response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

        [Route("SetPlugboard")]
        [HttpPost]
        public HttpResponseMessage SetPlugboard([FromBody] PlugboardDto plugboardDto)
        {
            HttpResponseMessage response;
            string json = JsonConvert.SerializeObject(plugboardDto, Formatting.Indented);
            //Dictionary<char, char> myDic = plugboardDto.Wiring;

            try
            {
                enigmaMachine.SetPlugboard(plugboardDto.Wiring);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
            response = Request.CreateResponse(HttpStatusCode.OK);

            return response;
        }

        [Route("Encrypt")]
        [HttpPost]
        public HttpResponseMessage Encrypt([FromBody] EnigmaMachineDto enigmaMachineDto)
        {
            HttpResponseMessage response;

            try
            {
                // TODO: don't use a static machine here.  It's not stateless
                enigmaMachine = new EnigmaMachine();
                enigmaMachine.SetPlugboard(enigmaMachineDto.Plugboard.Wiring);

                enigmaMachine.ChooseRotors(
                    enigmaMachineDto.Rotor1.RotorNum,
                    enigmaMachineDto.Rotor2.RotorNum,
                    enigmaMachineDto.Rotor3.RotorNum);

                enigmaMachine.SetRotorDials(
                    enigmaMachineDto.Rotor1.InitialDialSetting,
                    enigmaMachineDto.Rotor2.InitialDialSetting,
                    enigmaMachineDto.Rotor3.InitialDialSetting);

                enigmaMachine.LeaveWhiteSpace(true);

                string encodedMessage = enigmaMachine.Encode(enigmaMachineDto.Text);
                response = Request.CreateResponse(HttpStatusCode.OK, encodedMessage);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }

            return response;
        }
    }
}

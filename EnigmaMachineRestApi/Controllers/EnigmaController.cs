using Enigma;
using static Enigma.Enums.Enumerations;
using EnigmaMachineRestApi.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EnigmaMachineRestApi.Controllers
{
    [RoutePrefix("api/Enigma")]
    public class EnigmaController : ApiController
    {
        public static EnigmaMachine staticEnigmaMachine = new EnigmaMachine();

        /// <summary>
        /// Parses the rotors string sent in the Uri.  The first char becomes
        /// the rotor in the first slot of the enigma machine.  The second char
        /// becomes the rotor in the second slot, etc.
        /// </summary>
        /// <param name="rotors">String of length 3 containing numerals 1-5</param>
        /// <returns></returns>
        [Route("ChooseRotors")]
        [HttpPost]
        public HttpResponseMessage ChooseRotors([FromUri] string rotors)
        {
            HttpResponseMessage message;

            try
            {
                if (rotors.Length != 3)
                {
                    throw new Exception("must indicate 3 rotors");
                }

                if (!Int32.TryParse(rotors.Substring(0, 1), out int firstRotor))
                {
                    throw new Exception("Cound not parse first rotor");
                }

                if (!Int32.TryParse(rotors.Substring(1, 1), out int secondRotor))
                {
                    throw new Exception("Cound not parse second rotor");
                }

                if (!Int32.TryParse(rotors.Substring(2, 1), out int thirdRotor))
                {
                    throw new Exception("Cound not parse third rotor");
                }

                staticEnigmaMachine.ChooseRotors((RotorNumber)firstRotor, (RotorNumber)secondRotor, (RotorNumber)thirdRotor);
                message = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            return message;
        }

        [Route("SetRotor/{id:int:Range(1,3)}")]
        [HttpPost]
        public HttpResponseMessage SetRotor([FromBody] RotorDto rotorDto, [FromUri] int id)
        {
            HttpResponseMessage response;

            string json = JsonConvert.SerializeObject(rotorDto, Formatting.Indented);

            try
            {
                staticEnigmaMachine.SetRotorDial(id, rotorDto.InitialDialSetting);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

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
                staticEnigmaMachine.SetPlugboard(plugboardDto.Wiring);
                response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return response;
        }

        [Route("EncryptStatic")]
        [HttpPost]
        public HttpResponseMessage EncryptStatic([FromBody] string text)
        {
            HttpResponseMessage message;

            try
            {
                string encodedText = staticEnigmaMachine.Encode(text);
                message = Request.CreateResponse(HttpStatusCode.OK, encodedText);
            }
            catch (Exception ex)
            {
                message = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return message;
        }

        /// <summary>
        /// Uses settings in the EnigmaMachineDto to create and setup an enigma machine.
        /// It then encrypts the message with those settings.
        /// </summary>
        /// <param name="enigmaMachineDto">The entire enigma machine settings in one JSON object</param>
        /// <param name="leaveWhiteSpace">true to leave the white space in the text</param>
        /// <returns>Enigma encryted text</returns>
        [Route("Encrypt")]
        [HttpPost]
        public HttpResponseMessage Encrypt([FromBody] EnigmaMachineDto enigmaMachineDto, [FromUri] bool leaveWhiteSpace)
        {
            HttpResponseMessage response;
            EnigmaMachine enigmaMachine;

            try
            {
                enigmaMachine = new EnigmaMachine();
                enigmaMachine.SetPlugboard(enigmaMachineDto.Plugboard.Wiring);

                enigmaMachine.ChooseRotors(
                    (RotorNumber)enigmaMachineDto.Rotor1.RotorNum,
                    (RotorNumber)enigmaMachineDto.Rotor2.RotorNum,
                    (RotorNumber)enigmaMachineDto.Rotor3.RotorNum);

                enigmaMachine.SetRotorDials(
                    enigmaMachineDto.Rotor1.InitialDialSetting,
                    enigmaMachineDto.Rotor2.InitialDialSetting,
                    enigmaMachineDto.Rotor3.InitialDialSetting);

                enigmaMachine.LeaveWhiteSpace(leaveWhiteSpace);

                string encodedMessage = enigmaMachine.Encode(enigmaMachineDto.Text);

                response = Request.CreateResponse(HttpStatusCode.OK, encodedMessage);
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return response;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FlightTracker5.Models;
using FlightTracker5.Models.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Web.Script.Serialization;


namespace FlightTracker5.Controllers
{
    public class FlightsController : Controller
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private static readonly HttpClient client;


        static FlightsController()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AllowAutoRedirect = false
            };
            client = new HttpClient(handler);
            //Local Port
            client.BaseAddress = new Uri("https://localhost:44369/api/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Flights/List
        public ActionResult List()
        {
            FlightsDataController controller = new FlightsDataController();
            IEnumerable<FlightDto> flightDtos = controller.GetFlights();
            return View(flightDtos);

            //string url = "flightsdata/getflights";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    IEnumerable<FlightDto> SelectedFlights = response.Content.ReadAsAsync<IEnumerable<FlightDto>>().Result;
            //    return View(SelectedFlights);
            //}
            //else
            //{
            //    return RedirectToAction("Error");
            //}




            /*
                    // GET: Player/Details/5
                    public ActionResult Details(int id)
                    {
                        ShowPlayer ViewModel = new ShowPlayer();
                        string url = "playerdata/findplayer/" + id;
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        //Can catch the status code (200 OK, 301 REDIRECT), etc.
                        //Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {
                            //Put data into player data transfer object
                            PlayerDto SelectedPlayer = response.Content.ReadAsAsync<PlayerDto>().Result;
                            ViewModel.player = SelectedPlayer;


                            url = "playerdata/findteamforplayer/" + id;
                            response = client.GetAsync(url).Result;
                            TeamDto SelectedTeam = response.Content.ReadAsAsync<TeamDto>().Result;
                            ViewModel.team = SelectedTeam;

                            return View(ViewModel);
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }

                    // GET: Player/Create
                    public ActionResult Create()
                    {
                        UpdatePlayer ViewModel = new UpdatePlayer();
                        //get information about teams this player COULD play for.
                        string url = "teamdata/getteams";
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        IEnumerable<TeamDto> PotentialTeams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result;
                        ViewModel.allteams = PotentialTeams;

                        return View(ViewModel);
                    }

                    // POST: Player/Create
                    [HttpPost]
                    [ValidateAntiForgeryToken()]
                    public ActionResult Create(Player PlayerInfo)
                    {
                        Debug.WriteLine(PlayerInfo.PlayerFirstName);
                        string url = "playerdata/addplayer";
                        Debug.WriteLine(jss.Serialize(PlayerInfo));
                        HttpContent content = new StringContent(jss.Serialize(PlayerInfo));
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = client.PostAsync(url, content).Result;

                        if (response.IsSuccessStatusCode)
                        {

                            int playerid = response.Content.ReadAsAsync<int>().Result;
                            return RedirectToAction("Details", new { id = playerid });
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }


                    }

                    // GET: Player/Edit/5
                    public ActionResult Edit(int id)
                    {
                        UpdatePlayer ViewModel = new UpdatePlayer();

                        string url = "playerdata/findplayer/" + id;
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        //Can catch the status code (200 OK, 301 REDIRECT), etc.
                        //Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {
                            //Put data into player data transfer object
                            PlayerDto SelectedPlayer = response.Content.ReadAsAsync<PlayerDto>().Result;
                            ViewModel.player = SelectedPlayer;

                            //get information about teams this player COULD play for.
                            url = "teamdata/getteams";
                            response = client.GetAsync(url).Result;
                            IEnumerable<TeamDto> PotentialTeams = response.Content.ReadAsAsync<IEnumerable<TeamDto>>().Result;
                            ViewModel.allteams = PotentialTeams;

                            return View(ViewModel);
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }

                    // POST: Player/Edit/5
                    [HttpPost]
                    [ValidateAntiForgeryToken()]
                    public ActionResult Edit(int id, Player PlayerInfo, HttpPostedFileBase PlayerPic)
                    {
                        Debug.WriteLine(PlayerInfo.PlayerFirstName);
                        string url = "playerdata/updateplayer/" + id;
                        Debug.WriteLine(jss.Serialize(PlayerInfo));
                        HttpContent content = new StringContent(jss.Serialize(PlayerInfo));
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        HttpResponseMessage response = client.PostAsync(url, content).Result;
                        Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {

                            //Send over image data for player
                            url = "playerdata/updateplayerpic/" + id;
                            Debug.WriteLine("Received player picture " + PlayerPic.FileName);

                            MultipartFormDataContent requestcontent = new MultipartFormDataContent();
                            HttpContent imagecontent = new StreamContent(PlayerPic.InputStream);
                            requestcontent.Add(imagecontent, "PlayerPic", PlayerPic.FileName);
                            response = client.PostAsync(url, requestcontent).Result;

                            return RedirectToAction("Details", new { id = id });
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }

                    // GET: Player/Delete/5
                    [HttpGet]
                    public ActionResult DeleteConfirm(int id)
                    {
                        string url = "playerdata/findplayer/" + id;
                        HttpResponseMessage response = client.GetAsync(url).Result;
                        //Can catch the status code (200 OK, 301 REDIRECT), etc.
                        //Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {
                            //Put data into player data transfer object
                            PlayerDto SelectedPlayer = response.Content.ReadAsAsync<PlayerDto>().Result;
                            return View(SelectedPlayer);
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }

                    // POST: Player/Delete/5
                    [HttpPost]
                    [ValidateAntiForgeryToken()]
                    public ActionResult Delete(int id)
                    {
                        string url = "playerdata/deleteplayer/" + id;
                        //post body is empty
                        HttpContent content = new StringContent("");
                        HttpResponseMessage response = client.PostAsync(url, content).Result;
                        //Can catch the status code (200 OK, 301 REDIRECT), etc.
                        //Debug.WriteLine(response.StatusCode);
                        if (response.IsSuccessStatusCode)
                        {

                            return RedirectToAction("List");
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }*/


        }
        public ActionResult Error()
        {
            return View();
        }
    }
}
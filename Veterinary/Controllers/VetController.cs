using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Veterinary.Models;

namespace Veterinary.Controllers
{
    public class VetController : Controller
    {
        public IActionResult Index()
        {
            var request = new RestRequest("vet", Method.Get);
            var response = Client._client.Execute(request);
            return View(response.Content);
        }
        [HttpGet]
        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddClient(Owner owner)
        {
            var request = new RestRequest("vet/clients", Method.Post);
            var jsonBody = JsonConvert.SerializeObject(owner).ToString();
            request.AddBody(jsonBody);
            var response = Client._client.Execute(request);
            return RedirectToAction("GetClients");
        }
        [HttpGet]
        public IActionResult AddPet(int id)
        {
            TempData["ownerId"] = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddPet(Pet pet)
        {
            pet.OwnerId = int.Parse(TempData["ownerId"].ToString());
            var request = new RestRequest("vet/pets", Method.Post);
            //var jsonBody = JsonConvert.SerializeObject(pet).ToString();
            request.AddBody(pet);
            var response = Client._client.Execute(request);
            return RedirectToAction("GetPets", "Vet", new { id = TempData["ownerId"].ToString() });
        }
        [HttpGet]
        public IActionResult GetClients()
        {
            var request = new RestRequest("vet/clients", Method.Get);
            var response = Client._client.Execute(request);
            var clients = JsonConvert.DeserializeObject<IEnumerable<Owner>>(response.Content);
            return View("Clients",clients);
        }
        [HttpGet]
        public async Task<IActionResult> GetPets(int id)
        {
            TempData["ownerId"] = id;
            var request = new RestRequest("vet/pets/{id}", Method.Get);
            request.AddUrlSegment("id", id.ToString());
            //var response = await Client._client.Execute(request);
            var response = Client._client.Execute(request);
            var pets = JsonConvert.DeserializeObject<IEnumerable<Pet>>(response.Content);
            return View("Pets", pets);
        }

        [HttpGet]
        public IActionResult MedicalDetails(int id)
        {
            TempData["petId"] = id;
            var request = new RestRequest("vet/pets/{id}/details", Method.Get);
            request.AddUrlSegment("id", id.ToString());
            var response = Client._client.Execute(request);
            var details = JsonConvert.DeserializeObject<MedicalDetails>(response.Content);
            return View("MedicalDetails", details);
        }

        [HttpGet]
        public IActionResult AddPetOperation(int id)
        {
            TempData["petId"] = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddPetOperation(Operation operation)
        {
            var id = TempData["petId"].ToString();
            operation.PetId = int.Parse(id);
            var request = new RestRequest("vet/pets/{id}/operation", Method.Post);
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(operation);
            var response = Client._client.Execute(request);
            return RedirectToAction("MedicalDetails", "Vet", new {id = id});
        }

        [HttpGet]
        public IActionResult AddPetVaccine(int id)
		{
            TempData["petId"] = id;
            return View();
		}

		[HttpPost]
        public IActionResult AddPetVaccine(Vaccine vaccine)
		{
            var id = TempData["petId"].ToString();
            vaccine.PetId = int.Parse(id);
            var request = new RestRequest("vet/pets/{id}/vaccine", Method.Post);
            request.AddUrlSegment("id", id.ToString());
            request.AddBody(vaccine);
            var response = Client._client.Execute(request);
            return RedirectToAction("MedicalDetails", "Vet", new { id = id });
        }

        [HttpGet]
        public IActionResult EditOperation(int id)
        {
            var request = new RestRequest("operation/{operationId}", Method.Get);
            request.AddUrlSegment("operationId", id.ToString());
            var response = Client._client.Execute(request);
            var operation = JsonConvert.DeserializeObject<Operation>(response.Content);
            return View("EditOperation", operation);
        }

        [HttpPut]
        public IActionResult EditOperation(Operation operation)
        {
            var request = new RestRequest("operation/{operationId}", Method.Put);
            request.AddUrlSegment("operationId", operation.OperationId.ToString());
            var response = Client._client.Execute(request);
            return RedirectToAction("MedicalDetails", "Vet", new { id = TempData["petId"].ToString() });
        }

        [HttpGet]
        public IActionResult EditVaccine(int id)
        {
            var request = new RestRequest("vaccine/{vaccineId}", Method.Get);
            request.AddUrlSegment("vaccineId", id.ToString());
            var response = Client._client.Execute(request);
            var vaccine = JsonConvert.DeserializeObject<Vaccine>(response.Content);
            return View("EditVaccine", vaccine);
        }

        [HttpPut]
        public IActionResult EditVaccine(Vaccine vaccine)
        {
            var request = new RestRequest("vaccine/{vaccineId}", Method.Put);
            request.AddUrlSegment("vaccineId", vaccine.VaccineId.ToString());
            var response = Client._client.Execute(request);
            return RedirectToAction("MedicalDetails", "Vet", new { id = TempData["petId"].ToString() });
        }

        [HttpDelete]
        public IActionResult RemoveOperation(int id)
        {
            var request = new RestRequest("operation/{operationId}", Method.Delete);
            request.AddUrlSegment("operationId", id.ToString());
            var response = Client._client.Execute(request);
            return RedirectToAction("MedicalDetails", "Vet", new { id = TempData["petId"].ToString() });
        }

        public IActionResult RemoveVaccine(int id)
        {
            var request = new RestRequest("vaccine/{vaccineId}", Method.Delete);
            request.AddUrlSegment("vaccineId", id.ToString());
            var response = Client._client.Execute(request);
            return RedirectToAction("MedicalDetails", "Vet", new { id = TempData["petId"].ToString() });
        }
    }
}

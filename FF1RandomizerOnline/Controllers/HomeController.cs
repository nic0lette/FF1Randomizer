using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FF1RandomizerOnline.Models;
using FF1Lib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using RomUtilities;

namespace FF1RandomizerOnline.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHostingEnvironment _environment;

		public HomeController(IHostingEnvironment environment)
		{
			_environment = environment;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			var betaString = _environment.IsDevelopment() ? " Beta" : "";
			ViewData["Title"] = "FF1 Randomizer Online " + FF1Rom.Version + betaString;
			ViewData["DebugOnlyPreset"] = _environment.IsDevelopment() ? "<a v-on:click.prevent=\"preset('DEBUG')\">DEBUG</a>," : "";

			// Make this alpha only, maybe?
			ViewData["BuildMeta"] = _environment.IsDevelopment() ?
				@"<h4>
					<b>WELCOME TO FFR DEVELOPMENT BUILDS - Good luck! You'll Need it!</b>
					<ul>
						<li style=""color: red; font-weight:bold"">DO NOT USE THIS SITE FOR LEAGUE RACES. USE THE PRODUCTION SITE HERE: <a href=""http://finalfantasyrandomizer.com"">http://finalfantasyrandomizer.com</a>.</li>
						<li>FFR development websites can be updated literally anytime with no agenda or stability guarantees.</li>
						<li>Features may appear and disappear as different developers work and refine the flags for them.</li>
					</ul>
				</h4>" : "";

		}

		public IActionResult Index()
		{
			return View();
		}
		
		public IActionResult WhatsNew()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Randomize()
		{
			var vm = new RandomizeViewModel {
				File = null,
				Seed = Blob.Random(4).ToHex(),
				Flags = Flags.FromJson(System.IO.File.ReadAllText(Path.Combine(Startup.GetPresetsDirectory(), "default.json")))
			};
			vm.FlagsInput = Flags.EncodeFlagsText(vm.Flags);

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Randomize(RandomizeViewModel viewModel)
		{
			// Easier to just early return here and not have to verify viewModel.File != null repeatedly.
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			if (viewModel.File.Length < 256 * 1024 || viewModel.File.Length > (256 + 8) * 1024)
			{
				ModelState.AddModelError("File", "Unexpected file length, FF1 ROM should be close to 256 kB.");
			}

			var rom = await FF1Rom.CreateAsync(viewModel.File.OpenReadStream());
			if (!rom.Validate())
			{
				ModelState.AddModelError("File", "File does not appear to be a valid FF1 NES ROM.");
			}

			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			var json = "{\"Dialog\":[{\"number\":\"36\",\"text\":\"The GRINCH was a good WHO, until..\"},{\"number\":\"3f\",\"text\":\"The GRINCH was a good WHO, until..\"},{\"number\":\"04\",\"text\":\"For fifty-three years\\nI've put up with it now!\\nI MUST stop this\\nChristmas from coming!\\nBut HOW?\"},{\"number\":\"8c\",\"text\":\"You're a mean one,\\nMr. Grinch\\nYou really are a heel\"},{\"number\":\"8d\",\"text\":\"You're as cuddly as a\\ncactus,\\nyou're as charming as\\nan eel,\\nMr. Grinch\"},{\"number\":\"8e\",\"text\":\"You're a bad banana with\\na greasy black peel!\"},{\"number\":\"8f\",\"text\":\"You're a monster,\\nMr. Grinch\\nYour heart's an\\nempty hole\"},{\"number\":\"90\",\"text\":\"Your brain is full\\nof spiders\"},{\"number\":\"91\",\"text\":\"You've got garlic\\nin your soul,\\nMr. Grinch\"},{\"number\":\"92\",\"text\":\"I wouldn't touch you\\nwith a..\"},{\"number\":\"93\",\"text\":\"thirty-nine-and-a-half\\nfoot pole!\"},{\"number\":\"94\",\"text\":\"You're a vile one,\\nMr. Grinch\\n\"},{\"number\":\"95\",\"text\":\"You have termites in your\\nsmile\"},{\"number\":\"2b\",\"text\":\"You have all the tender\\nsweetness of a seasick\\ncrocodile, Mr. Grinch\"},{\"number\":\"2c\",\"text\":\"You have all the tender\\nsweetness of a seasick\\ncrocodile, Mr. Grinch\"},{\"number\":\"a0\",\"text\":\"Given a choice between\\nthe two of you\\nI'd take the\\nseasick crocodile!\"},{\"number\":\"2e\",\"text\":\"'PoohPooh to the Whos!'\\nhe was grinchishly\\nhumming. 'They're\\nfinding out now that\\nno Christmas is coming!'\"},{\"number\":\"2f\",\"text\":\"They're just waking up!\\nI know just what they'll\\ndo! Their mouths will\\nhang open a minute or\\ntwo,\"},{\"number\":\"30\",\"text\":\"'Then the Whos down in\\nWhoville will all cry\\nBooHoo! That's a noise'\\ngrinned the Grinch\\n'That I simply MUST\\nhear!'\"},{\"number\":\"4d\",\"text\":\"Fahoo forays,\\nDahoo dorays\\nWelcome Christmas!\\nCome this way\"},{\"number\":\"4e\",\"text\":\"Fahoo forays,\\nDahoo dorays\\nWelcome Christmas!\\nChristmas Day\"},{\"number\":\"4f\",\"text\":\"Welcome, welcome,\\nFahoo ramus\"},{\"number\":\"50\",\"text\":\"Welcome, welcome,\\nDahoo damus\"},{\"number\":\"51\",\"text\":\"Fahoo forays,\\nDahoo dorays...\"},{\"number\":\"fa\",\"text\":\"With a flash the\\nGRINCH ransacked all\\nof the Whos' houses..\\nLeaving crumbs much\\ntoo small for all\\nof the Whos' mouses!\"},{\"number\":\"96\",\"text\":\"You're as cuddly as a\\ncactus,\\nyou're as charming as\\nan eel,\\nMr. Grinch\"},{\"number\":\"97\",\"text\":\"You're a bad banana with\\na greasy black peel!\"},{\"number\":\"98\",\"text\":\"You're a monster,\\nMr. Grinch\\nYour heart's an\\nempty hole\"},{\"number\":\"99\",\"text\":\"Your brain is full\\nof spiders\"},{\"number\":\"9A\",\"text\":\"You've got garlic\\nin your soul,\\nMr. Grinch\"},{\"number\":\"9B\",\"text\":\"I wouldn't touch you\\nwith a..\"},{\"number\":\"9C\",\"text\":\"Thirty-nine-and-a-half\\nfoot pole!\"},{\"number\":\"9D\",\"text\":\"You're a vile one,\\nMr. Grinch\\n\"},{\"number\":\"9E\",\"text\":\"You have termites in your\\nsmile\"},{\"number\":\"FB\",\"text\":\"Trim up the tree\\nwith Christmas stuff\\nLike bingle balls,\\nand whofoo fluff.\"},{\"number\":\"FC\",\"text\":\"Hang pantookas\\non the ceilings\\nPile pankunas\\non the floor\"},{\"number\":\"FD\",\"text\":\"Trim up your pets\\nwith fuzzle fuzz\\nAnd whiffer bloofs,\\nand wuzzle wuzz\"},{\"number\":\"FE\",\"text\":\"Trim every blessed\\nneedle on the\\nblessed Christmas\\ntree\"},{\"number\":\"21\",\"text\":\"Santy Claus, why?\\nWhy are you taking\\nour Christmas tree?\\nWHY?\"},{\"number\":\"22\",\"text\":\"There's a light on this\\ntree that won't light on\\none side.\"},{\"number\":\"32\",\"text\":\"Yes Sir!!\\nI belong to the Honor\\nGuard of Whoville.\"},{\"number\":\"3F\",\"text\":\"Reports say the GRINCH\\nholds the Princess\\nto the northwest.\"},{\"number\":\"48\",\"text\":\"This is Whoville,\\nthe dream city.\"},{\"number\":\"49\",\"text\":\"North of Whoville lives\\na witch named Matoya.\"}]}";
			rom.Randomize(Blob.FromHex(viewModel.Seed), viewModel.Flags, json);

			var filename = viewModel.File.FileName;
			var pathIndex = filename.LastIndexOfAny(new[] { '\\', '/' });
			filename = pathIndex == -1 ? filename : filename.Substring(pathIndex + 1);

			var extensionIndex = filename.LastIndexOf('.');
			var newFilename = extensionIndex == -1 ? filename : filename.Substring(0, extensionIndex);
			newFilename = $"{newFilename}_{viewModel.Seed}_{Flags.EncodeFlagsText(viewModel.Flags)}.nes";

			Response.StatusCode = 200;
			Response.ContentLength = rom.TotalLength;
			Response.ContentType = "application/octet-stream";
			Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{newFilename}\"");

			await rom.SaveAsync(Response.Body);
			Response.Body.Close();

			return new EmptyResult();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}

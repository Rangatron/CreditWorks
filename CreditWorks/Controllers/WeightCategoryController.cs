using CreditWorks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CreditWorks.Controllers
{
    public class WeightCategoryController : Controller
    {
        private DatabaseContext Context { get; }
        public WeightCategoryController(DatabaseContext _context)
        {
            Context = _context;
        }

        public IActionResult Index()
        {
            List<WeightCategories> categories = (from w in Context.WeightCategories select w).ToList();
            return View(categories);
        }

        public IActionResult Edit(int id)
        {
            try
            {
                WeightCategories category = (from c in Context.WeightCategories where c.Id == id select c).First();
                return View(category);
            }
            catch (Exception e)
            {
                TempData["msg"] = "<script>alert('Failed to open editing page: " + e + "');</script>";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(WeightCategories category)
        {
            if (ModelState.IsValid)
            {
                if (category.StartWeight < 0 || (category.EndWeight < 0 && category.EndWeight != null))
                {
                    TempData["msg"] = "<script>alert('Could not update category. \\nPlease Ensure that start and end wights aern't negative.');</script>";
                    return RedirectToAction("Edit");
                }
                var existing = Context.Update(category);
                if (existing == null)
                {
                    TempData["msg"] = "<script>alert('Could not update category. \\nCategory doesn't exist');</script>";
                    return RedirectToAction("Edit");
                }
                List<WeightCategories> allCategories = (from c in Context.WeightCategories select c).OrderBy(c => c.StartWeight).ToList();
                bool previousGap = allCategories.Where(x => x.EndWeight == category.StartWeight).Any();

                if (!previousGap)
                {
                    WeightCategories previous = allCategories.Where(x => x.EndWeight < category.StartWeight).OrderByDescending(x => x.EndWeight).FirstOrDefault() ?? allCategories.Where(x => x.EndWeight > category.StartWeight && x.EndWeight < category.EndWeight).OrderBy(x => x.EndWeight).FirstOrDefault();
                    if (previous != null)
                    {
                        previous.EndWeight = category.StartWeight;
                        Context.Update(previous);
                    }
                }

                bool nextGap = allCategories.Where(x => x.StartWeight == category.EndWeight).Any();

                if (!nextGap)
                {
                    WeightCategories next = allCategories.Where(x => x.StartWeight < category.EndWeight && x.StartWeight > category.StartWeight).OrderByDescending(x => x.StartWeight).FirstOrDefault() ?? allCategories.Where(x => x.StartWeight > category.EndWeight).OrderBy(x => x.StartWeight).FirstOrDefault();
                    if(next != null)
                    {
                        next.StartWeight = category.EndWeight ?? 0;
                        Context.Update(next);
                    }
                }

                for (int i = 0; i < allCategories.Count; i++)
                {
                    if (category.StartWeight < allCategories[i].StartWeight && (category.EndWeight > allCategories[i].EndWeight || category.EndWeight == null))
                    {
                        allCategories[i].StartWeight = i-1 >= 0 ? allCategories[i - 1].EndWeight ?? 0 : allCategories[i].StartWeight;
                        allCategories[i].EndWeight = category.StartWeight;
                        Context.Update(allCategories[i]);
                        i = 0;
                    }
                    
                }
                Context.SaveChanges();
                TempData["msg"] = "<script>alert('Category Saved. \\nAll overlaps and gaps have been filled');</script>";
                return RedirectToAction("Index");
            }
            TempData["msg"] = "<script>alert('Could not update category. \\nPlease ensure that all fields are filled out correctly');</script>";
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public ActionResult Create(WeightCategories category)
        {
            if (ModelState.IsValid)
            {
                if (category.StartWeight < 0 || (category.EndWeight < 0 && category.EndWeight != null)) return RedirectToAction("Index");
                this.Context.WeightCategories.Add(category);
                List<WeightCategories> allCategories = (from c in Context.WeightCategories select c).OrderBy(c => c.StartWeight).ToList();
                bool previousGap = allCategories.Where(x => x.EndWeight == category.StartWeight).Any();

                if (!previousGap)
                {
                    WeightCategories previous = allCategories.Where(x => x.EndWeight < category.StartWeight).OrderByDescending(x => x.EndWeight).FirstOrDefault() ?? allCategories.Where(x => x.EndWeight > category.StartWeight && x.EndWeight < category.EndWeight).OrderBy(x => x.EndWeight).FirstOrDefault();
                    if (previous != null)
                    {
                        previous.EndWeight = category.StartWeight;
                        Context.Update(previous);
                    }
                }

                bool nextGap = allCategories.Where(x => x.StartWeight == category.EndWeight).Any();

                if (!nextGap)
                {
                    WeightCategories next = allCategories.Where(x => x.StartWeight < category.EndWeight && x.StartWeight > category.StartWeight).OrderByDescending(x => x.StartWeight).FirstOrDefault() ?? allCategories.Where(x => x.StartWeight > category.EndWeight).OrderBy(x => x.StartWeight).FirstOrDefault();
                    if (next != null)
                    {
                        next.StartWeight = category.EndWeight ?? 0;
                        Context.Update(next);
                    }
                }

                for (int i = 0; i < allCategories.Count; i++)
                {
                    if(category.StartWeight < allCategories[i].StartWeight && (category.EndWeight > allCategories[i].EndWeight || category.EndWeight == null))
                    {
                        allCategories[i].StartWeight = i - 1 >= 0 ? allCategories[i - 1].EndWeight ?? 0 : allCategories[i].StartWeight;
                        allCategories[i].EndWeight = category.StartWeight;
                        Context.Update(allCategories[i]);
                        i = 0;
                    }
                }
                this.Context.SaveChanges();
                TempData["msg"] = "<script>alert('Category Created. \\nAll overlaps and gaps have been filled');</script>";
                return RedirectToAction("Index");
            }
            TempData["msg"] = "<script>alert('Could not create category. \\nPlease ensure that all fields are filled out correctly');</script>";
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            WeightCategories category = Context.WeightCategories.Find(id);
            if (category == null)
            {
                TempData["msg"] = "<script>alert('Could not delete category. \\nCategory doesn't exist');</script>";
                return RedirectToAction("Index");
            }
            List<WeightCategories> allCategories = (from c in Context.WeightCategories select c).OrderBy(c => c.StartWeight).ToList();
            for (int i = 0; i < allCategories.Count - 1; i++)
            {
                if (allCategories[i].Id == id && allCategories[i - 1] != null && allCategories[i + 1] != null)
                {
                    allCategories[i - 1].EndWeight = allCategories[i + 1].StartWeight;
                    Context.Update(allCategories[i - 1]);
                }
            }
            Context.WeightCategories.Remove(category);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

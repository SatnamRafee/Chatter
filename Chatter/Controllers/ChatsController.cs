using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chatter.Models;


namespace Chatter.Controllers
{
    public class ChatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        

        // GET: Chats
        public ActionResult Index()
        {
            //Chat chat = db.Chats.Include(i => i.ApplicationUser.ChatName);

            ////Daniel:We can populate our new view model entity with a linq query. We have a lot of flexibility
            //var viewModel = new ChatViewModel
            //{
            //    //We can reuse the comment form from a couple of lines above
            //    Chat = chat,
            //    //We don't want to pull back all the procedures, just the one with the same priority as our comment
            //    ApplicationUser = (from p in db.Users
            //                       where p.ChatName == chat.ApplicationUser.ChatName
            //                       select p).First()
            //};
            //return View(viewModel);

            return View(db.Chats.ToList());

            // GET: Chats/Details/5
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ChatViewModel
            {
                //We can reuse the chat from a couple of lines above
                Chat = chat,
                
                //We don't want to pull back all the users, just the one with the same SOMETHING as our chat
                ApplicationUser = (from p in db.Users
                                   where p.ChatName == chat.PublishedBy
                                   select p).First()
            };
            return View(viewModel);


            //return View(chat);

        }

        // GET: Chats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChatID,ChatBody,PublishedBy")] Chat chat)
        {

            if (ModelState.IsValid)
            {
                db.Chats.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chat);
        }

        // GET: Chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: Chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChatID,ChatBody")] Chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chat);
        }

        // GET: Chats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chat chat = db.Chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: Chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chat chat = db.Chats.Find(id);
            db.Chats.Remove(chat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using ContactsManage.Filters;
using ContactsManage.Helper;
using ContactsManage.Models;
using ContactsManage.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManage.Controllers
{
	[PageLoggedInUser]
	public class ContactController : Controller
	{
		private readonly IContactRepositorio _contactRepository;
		private readonly Helper.ISession _session;

		public ContactController(IContactRepositorio contactRepository, Helper.ISession session)
		{
			_contactRepository = contactRepository;
			_session = session;
		}

		public IActionResult Index()
		{
			User userLogged = _session.FindSession();
			List<Contact> contacts = _contactRepository.FindUserContactsById(userLogged.Id);
			return View(contacts);
		}

		public IActionResult CreateContact()
		{
			return View();
		}

		[HttpPost]
		public IActionResult CreateContact(Contact contact)
		{
			try
			{
				if (ModelState.IsValid)
				{
					User userLogged = _session.FindSession();
					contact.UserId = userLogged.Id;
					_contactRepository.AddContact(contact);
					TempData["MsgSuccess"] = $"Contato {contact.Name} adicionado com sucesso.";
					return RedirectToAction("Index");
				}
				return View(contact);
			}
			catch (Exception error)
			{
				TempData["MsgError"] = $"Ops!! Não foi possível cadastrar seu contato, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
				return RedirectToAction("Index");
			}
		}

		public IActionResult UpdateContact(int id)
		{
			Contact contact = _contactRepository.FindById(id);
			User user = _session.FindSession();
			if (contact.UserId != user.Id)
			{
				return RedirectToAction("Index", "Contato");
			}

			return View(contact);
		}

		[HttpPost]
		public IActionResult UpdateContact(Contact contact)
		{
			try
			{
				Contact updateContact = _contactRepository.FindById(contact.Id);
				if (updateContact.Id != contact.Id)
				{
					TempData["MsgError"] = "Ops!! Este contato não existe para ser atualizado.";
					return RedirectToAction("Index");
				}
				if (ModelState.IsValid)
				{
					User userLogged = _session.FindSession();
					contact.UserId = userLogged.Id;

					_contactRepository.UpdateContact(contact);
					TempData["MsgSuccess"] = $"Contato {contact.Name} atualizado com sucesso.";
					return RedirectToAction("Index");
				}
				return View("Editar", contact);
			}
			catch (Exception error)
			{
				TempData["MsgError"] = $"Ops!! Não foi possível atualizar seu contato, tente novamente ou entre em contato com o suporte, detalhes do erro: {error.Message}";
				return RedirectToAction("Index");
			}

		}

		public IActionResult ConfirmDelete(int id)
		{
			Contact contact = _contactRepository.FindById(id);

			User user = _session.FindSession();
			if (contact.UserId != user.Id)
			{
				return RedirectToAction("Index", "Contato");
			}
			return View(contact);
		}

		public IActionResult DeleteContact(int id)
		{
			try
			{
				Contact deleteContact = _contactRepository.FindById(id);
				if (deleteContact.Id == 0)
				{
					TempData["MsgError"] = "Ops!! Este contato não existe para ser apagado.";
					return RedirectToAction("Index");
				}

				_contactRepository.DeleteContact(deleteContact.Id);
				TempData["MsgSuccess"] = $"Contato {deleteContact.Name} apagado com sucesso.";
				return RedirectToAction("Index");

			}
			catch (Exception ex)
			{
				TempData["MsgError"] = $"Ops!! Não foi possível apagar seu contato, tente novamente ou entre em contato com o suporte, detalhes do erro: {ex}";
				return RedirectToAction("Index");
			}
		}
	}
}

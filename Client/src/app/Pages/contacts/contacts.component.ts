import { Component, OnInit, ViewChild } from '@angular/core';
import { Contact } from 'src/app/Interface/CommonInterface';
import { AddEditContactComponent } from '../add-edit-contact/add-edit-contact.component';
import { ApiService } from 'src/app/Services/api.service';
import { NotificationService } from 'src/app/Services/notification.service';
import { ConfirmDialogComponent } from 'src/app/CommonComponents/confirm-dialog/confirm-dialog.component';
import { t } from 'i18next';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})
export class ContactsComponent implements OnInit {
  @ViewChild(AddEditContactComponent) editModal!: AddEditContactComponent;
  @ViewChild('confirmDelete') confirmDialog!: ConfirmDialogComponent;
  contacts: Contact[] = []

  sortField: string | null = null;
  sortAsc = true;
  contactToDelete: Contact | null = null;

  constructor(private service: ApiService, private notification: NotificationService) { }

  ngOnInit(): void {
    this.getallContacts();
  }

  getallContacts() {
    this.service.getContacts().subscribe({
      next: (response: any) => {
        this.contacts = response;
      },
      error: (error) => {
        console.error('Error fetching contacts:', error);
      }
    });
  }

  sortBy(field: string) {
    if (this.sortField === field) {
      this.sortAsc = !this.sortAsc;
    } else {
      this.sortField = field;
      this.sortAsc = true;
    }

    this.contacts.sort((a, b) => {
      const valA = (a as any)[field]?.toString().toLowerCase() || '';
      const valB = (b as any)[field]?.toString().toLowerCase() || '';
      return this.sortAsc ? valA.localeCompare(valB) : valB.localeCompare(valA);
    });
  }

  editContact(contact: Contact) {
    this.editModal.contact = contact;
    this.editModal.openModal();
  }

  onContactUpdated(data: { contact: Contact }) {
    this.service.addOrUpdateContact(data.contact).subscribe({
      next: () => {
        this.notification.success(t(data.contact.Id ? 'Contact_updated' : 'Contact_added'));
        this.getallContacts();
      },
      error: (error) => {
        console.error('Error updating contact:', error);
      }
    });
  }

  addContact() {
    this.editModal.contact = null;
    this.editModal.openModal();
  }

  deleteContact(): void {
    if (this.contactToDelete)
      this.service.removeContact([this.contactToDelete]).subscribe({
        next: () => {
          // this.notification.success('Contact deleted successfully');
          this.notification.success(t('Contact_deleted'));
          this.getallContacts();
        },
        error: (error) => {
          console.error('Error deleting contact:', error);
        }
      });
  }

  openDeleteModal(contact: Contact): void {
    this.contactToDelete = contact;
    this.confirmDialog.show();
  }

  cancelDelete() {
    this.contactToDelete = null;
    this.confirmDialog.onCancel();
  }

}

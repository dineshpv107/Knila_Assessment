import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Contact } from 'src/app/Interface/CommonInterface';
declare var bootstrap: any;

@Component({
  selector: 'app-add-edit-contact',
  templateUrl: './add-edit-contact.component.html',
  styleUrls: ['./add-edit-contact.component.scss']
})
export class AddEditContactComponent {

  @Input() contact: Contact | null = null;
  @Output() update = new EventEmitter<{ contact: Contact }>();
  formFieldOrder: string[] = [
    'FirstName',
    'LastName',
    'Email',
    'PhoneNumber',
    'Address',
    'City',
    'State',
    'Country',
    'PostalCode'
  ];

  editForm: FormGroup;
  modalRef: any;
  submitted = false;

  constructor(private fb: FormBuilder) {
    this.editForm = this.fb.group({
      FirstName: ['', Validators.required],
      LastName: ['', Validators.required],
      Email: ['', [Validators.email]],
      PhoneNumber: ['', [Validators.maxLength(10), Validators.pattern('^[0-9]*$'), Validators.required]],
      Address: [''],
      City: [''],
      State: [''],
      Country: [''],
      PostalCode: ['', [Validators.maxLength(6), Validators.pattern('^[0-9]*$')]]
    });
  }

  openModal() {
    this.editForm.reset();
    if (this.contact) {
      this.editForm.patchValue(this.contact);
    }

    const modalEl = document.getElementById('editContactModal');
    this.modalRef = new bootstrap.Modal(modalEl);
    this.modalRef.show();
  }

  closeModal() {
    this.submitted = false;
    this.modalRef?.hide();
  }

  onSubmit() {
    this.submitted = true;
    if (this.editForm.valid) {
      this.update.emit({ contact: { ...this.editForm.value, Id: this?.contact?.Id ?? 0 } });
      this.closeModal();
    }
  }

  formatLabel(key: string): string {
    return key
      .replace(/([A-Z])/g, ' $1')
      .replace(/^./, str => str.toUpperCase())
      .trim();
  }

  getErrorMessage(fieldName: string): string {
    const control = this.editForm.get(fieldName);
    if (!control || !control.errors) return '';

    if (control.hasError('required')) {
      return `${this.formatLabel(fieldName)} is required.`;
    }
    if (control.hasError('email')) {
      return `Please enter a valid email address.`;
    }
    if (control.hasError('maxlength')) {
      const requiredLength = control.getError('maxlength').requiredLength;
      return `${this.formatLabel(fieldName)} must be at most ${requiredLength} characters long.`;
    }
    if (control.hasError('minlength')) {
      const requiredLength = control.getError('minlength').requiredLength;
      return `${this.formatLabel(fieldName)} must be at least ${requiredLength} characters long.`;
    }
    if (control.hasError('pattern')) {
      return `${this.formatLabel(fieldName)} format is invalid.`;
    }

    return '';
  }


}

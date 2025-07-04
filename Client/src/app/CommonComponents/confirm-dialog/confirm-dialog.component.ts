import { Component, EventEmitter, Input, Output } from '@angular/core';
declare var bootstrap: any;

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
})
export class ConfirmDialogComponent {
  @Input() title: string = 'Confirm';
  @Input() message: string = 'Are you sure?';
  @Input() confirmText: string = 'Yes';
  @Input() cancelText: string = 'Cancel';

  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();
  modalInstance: any = null;

  show(): void {
    const modalEl = document.getElementById('confirmDialogModal');
    if (modalEl) {
      this.modalInstance = new bootstrap.Modal(modalEl);
      this.modalInstance.show();
    }
  }

  hide(): void {
    this.modalInstance?.hide();
  }

  onConfirm(): void {
    this.confirm.emit();
    this.hide();
  }

  onCancel(): void {
    this.cancel.emit();
    this.hide();
  }
}

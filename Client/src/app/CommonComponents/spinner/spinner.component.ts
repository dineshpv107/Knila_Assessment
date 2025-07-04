import { Component } from '@angular/core';

@Component({
  selector: 'app-spinner',
  template: `<div class="spinner-overlay" *ngIf="isLoading"></div>`,
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent {
  isLoading: boolean = true;
}
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SpinnerService } from './Services/spinner.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  isLoading = this.spinnerService.spinnerState$;
  window = window;

  constructor(private spinnerService: SpinnerService) { }

  ngOnInit() {
    this.isLoading = this.spinnerService.spinnerState$;
  }
}

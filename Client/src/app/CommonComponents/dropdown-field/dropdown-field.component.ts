import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-dropdown-field',
  templateUrl: './dropdown-field.component.html',
  styleUrls: ['./dropdown-field.component.scss']
})
export class DropdownFieldComponent implements OnInit {
  @Input() label: string = '';
  @Input() hint: string = '';
  @Input() isInvalid: boolean = false;
  @Input() id: string = 'select-' + Math.random().toString(36).substring(2, 8);
  @Input() options: { value: any; label: string }[] = [];
  @Input() value: any;
  @Input() placeholder: string = '-- Select --';

  @Output() valueChange = new EventEmitter<any>();

  constructor() { }

  ngOnInit(): void {
  }

  onChange(event: Event) {
    const target = event.target as HTMLSelectElement;
    this.valueChange.emit(target.value);
  }

}

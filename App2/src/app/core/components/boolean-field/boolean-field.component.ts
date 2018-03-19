import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'boolean-field',
  templateUrl: './boolean-field.component.html',
  styleUrls: ['./boolean-field.component.css']
})
export class BooleanFieldComponent {
  @Input() booleanValue: boolean;
  @Output() booleanValueChange = new EventEmitter<boolean>();
  @Input() label: string;
  @Input() tip: string;
  @Input() disabled: boolean = false;

  changeBooleanValue(): void {
    this.booleanValueChange.emit(this.booleanValue);
  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'text-field',
  templateUrl: './text-field.component.html',
  styleUrls: ['./text-field.component.css']
})
export class TextFieldComponent {
  @Input() text: string;
  @Output() textChange = new EventEmitter<string>();
  @Output() blur = new EventEmitter<string>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;
  @Input() fieldNgClass: string = '';

  changeText(newValue): void {
    this.text = newValue;
    this.textChange.emit(this.text);
  }

  onBlur(newValue): void {
    this.blur.emit(null);
  }
}

//ref: https://angular.io/guide/component-interaction

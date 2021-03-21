import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'text-area-field',
  templateUrl: './text-area-field.component.html',
  styleUrls: ['./text-area-field.component.css']
})
export class TextAreaFieldComponent {
  @Input() text: string;
  @Output() textChange = new EventEmitter<string>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeText(): void {
    this.textChange.emit(this.text);
  }
}

//ref: https://angular.io/guide/component-interaction

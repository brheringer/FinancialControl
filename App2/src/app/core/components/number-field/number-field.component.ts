import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'number-field',
  templateUrl: './number-field.component.html',
  styleUrls: ['./number-field.component.css']
})
export class NumberFieldComponent {
  @Input() number: number;
  @Output() numberChange = new EventEmitter<number>();
  @Output() blur = new EventEmitter<number>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeNumber(): void {
    this.numberChange.emit(this.number);
  }

  onBlur(): void {
    this.blur.emit(null);
  }


  toString(number: number): string {
    return number + ''; //TODO e o separador de decima?
  }

  fromString(strnumber: string): number {
    return Number(strnumber) || 0;
  }
}

//ref: https://angular.io/guide/component-interaction

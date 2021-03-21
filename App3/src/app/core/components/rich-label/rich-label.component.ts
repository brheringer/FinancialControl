import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'rich-label',
  templateUrl: './rich-label.component.html',
  styleUrls: ['./rich-label.component.css']
})
export class RichLabelComponent {
  @Input() label: string;
  @Input() tip: string;
  @Input() icon: string; //text, number, date, select, smart-search, boolean

  getIconUrl() {
    return `assets/${this.icon}_24px.svg`; //TODO rever
  }
}

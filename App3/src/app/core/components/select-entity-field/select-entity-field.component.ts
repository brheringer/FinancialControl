import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EntityReference } from '../../model/entity-reference';

@Component({
  selector: 'select-entity-field',
  templateUrl: './select-entity-field.component.html',
  styleUrls: ['./select-entity-field.component.css']
})
export class SelectEntityFieldComponent {
  @Input() item: EntityReference;
  @Output() itemChange = new EventEmitter<EntityReference>();
  @Input() items: Array<EntityReference>;
  @Output() blur = new EventEmitter<EntityReference>();
  @Input() label: string;
  @Input() tip: string;
  @Input() required: boolean = false;
  @Input() disabled: boolean = false;

  changeItem(): void {
    this.itemChange.emit(this.item);
  }

  onBlur(): void {
    this.blur.emit(null);
  }

  compareEntities(a, b): boolean {
    return a && b && a.autoId === b.autoId;
  }
}

//ref: https://angular.io/guide/component-interaction

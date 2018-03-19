import { Pipe, PipeTransform } from '@angular/core';
import { Entity } from '../model/entity';

@Pipe({ name: 'finEntityFormat' })
export class EntityFormatPipe implements PipeTransform {
  transform(entity: Entity): string {
    if (entity === null || typeof entity === 'undefined')
      return '';
    return entity.presentation;
  }
}

import { Entity } from '../core/model/Entity';
import { EntityStatus } from './entityStatus.model';

export class ResultCenter extends Entity {
  code: string;
  description: string;
  status: EntityStatus = new EntityStatus();
}

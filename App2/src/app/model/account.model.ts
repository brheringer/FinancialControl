import { Entity } from '../core/model/Entity';
import { EntityStatus } from './entityStatus.model';

export class Account extends Entity {
  structure: string;
  description: string;
  type: string;
  status: EntityStatus = new EntityStatus();
}

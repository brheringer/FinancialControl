import { Entity } from '../core/model/Entity';
import { EntityReference } from '../core/model/entity-reference';
import { EntityStatus } from './entityStatus.model';

export class EntryTemplate extends Entity {
  title: string;
  value: number;
  memo: string;
  account: EntityReference;
  center: EntityReference;
  status: EntityStatus = new EntityStatus();
}

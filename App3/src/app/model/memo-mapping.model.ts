import { Entity } from '../core/model/Entity';
import { EntityReference } from '../core/model/entity-reference';
import { EntityStatus } from './entityStatus.model';

export class MemoMapping extends Entity {
  incomingMemo: string;
  mappedMemo: string;
  mappedAccount: EntityReference;
  mappedCenter: EntityReference;
  status: EntityStatus = new EntityStatus();
}

import { Collection } from '../core/model/collection';
import { MemoMapping } from './memo-mapping.model';

export class MemosMappings extends Collection {
  mappings: Array<MemoMapping> = new Array<MemoMapping>();
}

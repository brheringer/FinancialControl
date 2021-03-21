import { Collection } from '../core/model/collection';
import { EntryTemplate } from './entry-template.model';

export class EntriesTemplates extends Collection {
  templates: Array<EntryTemplate> = new Array<EntryTemplate>();
}

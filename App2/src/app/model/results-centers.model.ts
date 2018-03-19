import { Collection } from '../core/model/collection';
import { ResultCenter } from './result-center.model';

export class ResultsCenters extends Collection {
  filterCode: string;
  filterDescription: string;
  resultsCenters: Array<ResultCenter> = new Array<ResultCenter>();
}

import { Pipe, PipeTransform } from '@angular/core';
import { Albom } from 'app/models/albom';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(alboms: Albom[], search: string = ''): Albom[] {
    if(!search.trim()) return alboms;
    return alboms.filter(albom => {return albom.title.includes(search)})
  }
}

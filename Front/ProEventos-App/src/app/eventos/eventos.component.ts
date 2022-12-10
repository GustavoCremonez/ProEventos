import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent {
  public eventos: any = [];
  public filteredEvents: any = [];

  private _filterList: string = '';
  hideImage: boolean = false;

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(filter: string) {
    this._filterList = filter;

    this.filteredEvents = this.filterList
      ? this.handleFilterEvents(this.filterList)
      : this.eventos;
  }

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:44391/api/Eventos').subscribe({
      next: this.handleUpdateEventos.bind(this),
      error: this.handleError.bind(this),
    });
  }

  public handleUpdateEventos(response: any): void {
    this.eventos = response;
    this.filteredEvents = response;
  }

  public handleError(error: Error): void {
    console.error(error);
  }

  public handleHideImage() {
    this.hideImage = !this.hideImage;
  }

  public handleFilterEvents(filter: string): any {
    filter = filter.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: any) =>
        evento.tema.toLocaleLowerCase().indexOf(filter) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filter) !== -1 ||
        evento.dataEvento.indexOf(filter) !== -1
    );
  }
}

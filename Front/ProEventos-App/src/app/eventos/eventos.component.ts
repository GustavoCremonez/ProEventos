import { Component } from '@angular/core';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent {
  public eventos: Evento[] = [];
  public filteredEvents: Evento[] = [];

  private _filterList: string = '';
  public hideImage: boolean = false;

  public get filterList(): string {
    return this._filterList;
  }

  public set filterList(filter: string) {
    this._filterList = filter;

    this.filteredEvents = this.filterList
      ? this.handleFilterEvents(this.filterList)
      : this.eventos;
  }

  constructor(private eventoService: EventoService) {}

  public ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: this.handleUpdateEventos.bind(this),
      error: this.handleError.bind(this),
    });
  }

  public handleUpdateEventos(Evento: Evento[]): void {
    this.eventos = Evento;
    this.filteredEvents = Evento;
  }

  public handleError(error: Error): void {
    console.error(error);
  }

  public handleHideImage(): void {
    this.hideImage = !this.hideImage;
  }

  public handleFilterEvents(filter: string): Evento[] {
    filter = filter.toLocaleLowerCase();

    return this.eventos.filter(
      (evento: any) =>
        evento.tema.toLocaleLowerCase().indexOf(filter) !== -1 ||
        evento.local.toLocaleLowerCase().indexOf(filter) !== -1 ||
        evento.dataEvento.indexOf(filter) !== -1
    );
  }
}

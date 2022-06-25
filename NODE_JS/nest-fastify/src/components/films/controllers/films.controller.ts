import { Controller, Get, Query } from '@nestjs/common';
import { FilmsService } from '../services/films/films.service';

@Controller('films')
export class FilmsController {
  constructor(private readonly filmsService: FilmsService) {}

  @Get('pg')
  get(@Query('take') take?: number) {
    return this.filmsService.getFilms(take);
  }
}

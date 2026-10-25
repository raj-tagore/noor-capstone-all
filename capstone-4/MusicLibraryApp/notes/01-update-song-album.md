# UpdateSongAlbum

Reconstructed from the photo in `capstone-4/ps-pics`. The photo is one method body, written from memory. Names, return numbers, and the `try`/`catch` follow the Polyclinic capstone (`CancelAppointment` and `usp_GetDoctorAppointment`). If tomorrow's table prints different integers, change only the integers.

## What the question is asking

Move one song from its current album to another album.

```csharp
public int UpdateSongAlbum(string songName, string newAlbumName)
```

| Step | What to do |
|---|---|
| 1 | Load the song with a LINQ query on `SongName`. |
| 2 | Song missing → `-1`. |
| 3 | Load the old album and the new album with `Find` (album name is the primary key). |
| 4 | New album missing → `-2`. |
| 5 | `CurrentSongs >= TotalSongs` on the new album → `-3`. There is no free slot. |
| 6 | Otherwise set `song.AlbumName = newAlbumName`, subtract 1 from the old album's `CurrentSongs`, add 1 to the new album's `CurrentSongs`, `SaveChanges()`, return `1`. |
| 7 | Any exception → `-99`. |

`TotalSongs` is the capacity. `CurrentSongs` is how many songs are already in the album. A free slot exists only when `CurrentSongs < TotalSongs`.

## What the photo got wrong

| Photo | Write this |
|---|---|
| `.ToList()` stored in a `Song` | `.FirstOrDefault()`. `ToList()` is a list, and an empty list is never `null`, so the missing-song branch would never run. |
| `p.songName`, `currentSongs`, `totalSongs` | `SongName`, `AlbumName`, `CurrentSongs`, `TotalSongs`. Generated models in this org are PascalCase. |
| `context.Album` | `context.Albums`. DbSets in Polyclinic and CureWell are plural (`Patients`, `Doctors`, `Surgeries`). `context.Songs` in the photo is already plural. |
| `ans` left blank | `1`, `-1`, `-2`, `-3` as in the table above. |
| No `try` / `catch` | Required. Soft misses stay inside `try`. The `catch` sets `-99`. |

## Copy the property names from the supplied model

The method logic does not change if the supplied class uses `SongID` or a slightly different DbSet name. The identifiers below have to match the context and the entity you are given:

- `context.Songs`, `context.Albums`
- `Song.SongName`, `Song.AlbumName`
- `Album.CurrentSongs`, `Album.TotalSongs`

## Same shape as CancelAppointment

Missing row → negative code inside `try`. Thrown error → `-99` in `catch`. `SaveChanges()` only on the success path, after the in-memory values are already changed. EF is tracking the song and both albums, so it sends the `UPDATE`s.

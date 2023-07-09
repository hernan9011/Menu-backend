using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
  
        public DbSet<Comanda> Comanda { get; set; }
        public DbSet<ComandaMercaderia> ComandaMercaderia { get; set; }
        public DbSet<FormaEntrega> FormaEntrega { get; set; }
        public DbSet<Mercaderia> Mercaderia { get; set; }
        public DbSet<TipoMercaderia> TipoMercaderia { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
                 : base(options)
        {
        }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comanda>(entity =>
            {
                entity.HasKey(s => s.ComandaId);
                entity.Property(s => s.FormaEntregaId).IsRequired();
                entity.Property(s => s.PrecioTotal).IsRequired();
                entity.Property(s => s.Fecha).IsRequired().HasColumnType("date");
                entity.HasOne<FormaEntrega>(o => o.FKFormaEntrega).WithMany(m => m.LsComanda).HasForeignKey(f => f.FormaEntregaId);
                entity.HasMany<ComandaMercaderia>(m => m.LsComandaMercaderia).WithOne(o => o.FKComanda);
            });

            modelBuilder.Entity<FormaEntrega>(entity =>
            {
                entity.HasKey(k => k.FormaEntregaId);
                entity.HasMany<Comanda>(m => m.LsComanda).WithOne(o => o.FKFormaEntrega);
                entity.Property(s => s.Descripcion).IsRequired().HasMaxLength(50);
                entity.HasData(new FormaEntrega
                {
                    FormaEntregaId = 1,
                    Descripcion = "SALON"
                });
                entity.HasData(new FormaEntrega
                {
                    FormaEntregaId = 2,
                    Descripcion = "DELIVERY"
                });
                entity.HasData(new FormaEntrega
                {
                    FormaEntregaId = 3,
                    Descripcion = "PEDIDOS YA"
                });
            });

            modelBuilder.Entity<ComandaMercaderia>(entity =>
            {
                entity.HasKey(k => k.ComandaMercaderiaId);
                entity.HasOne<Comanda>(o => o.FKComanda).WithMany(m => m.LsComandaMercaderia).HasForeignKey(f => f.ComandaId);
                entity.HasOne<Mercaderia>(o => o.FKMercaderia).WithMany(m => m.LsComandaMercaderia).HasForeignKey(f => f.MercaderiaId);
                entity.Property(s => s.ComandaId).IsRequired();
                entity.Property(s => s.MercaderiaId).IsRequired();
            });

            modelBuilder.Entity<Mercaderia>(entity =>
            {
                entity.HasKey(k => k.MercaderiaId);
                entity.Property(t => t.MercaderiaId).ValueGeneratedOnAdd();
                entity.HasMany<ComandaMercaderia>(o => o.LsComandaMercaderia).WithOne(o => o.FKMercaderia);
                entity.HasOne<TipoMercaderia>(o => o.FKTipoMercaderia).WithMany(m => m.LsMercaderia).HasForeignKey(f => f.TipoMercaderiaId);
                entity.Property(s => s.Nombre).IsRequired().HasMaxLength(50);
                entity.Property(s => s.TipoMercaderiaId).IsRequired();
                entity.Property(s => s.Precio).IsRequired();
                entity.Property(s => s.Ingredientes).IsRequired().HasMaxLength(255);
                entity.Property(s => s.Preparacion).IsRequired().HasMaxLength(255);
                entity.Property(s => s.Imagen).IsRequired().HasMaxLength(255);
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 1,
                    Nombre = "SANDWICH DE ATUN",
                    TipoMercaderiaId = 6,
                    Precio = 1500,
                    Ingredientes = " 4 rebanadas de pan de molde sin corteza,150 g. de atún natural en conserva,1/4 de cebolla roja cortada en cuadritos,2 cucharadas de mayonesa,Sal y pimienta al gusto",
                    Preparacion = "En un bol agregamos el atún, la cebolla roja, la mayonesa, un poco de sal, un poco de pimienta y mezclamos todo muy bien hasta integrar.Agregamos relleno en las rebanadas de pan y luego le colocamos la otra rebanada encima.",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2020/12/sandwich-1580348_1280.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 2,
                    Nombre = "SANDWICH DE HUEVO",
                    TipoMercaderiaId = 6,
                    Precio = 2500,
                    Ingredientes = "1 pieza de pan baguette de unos 30 centímetros de largo,2 huevos,1/2 to mate,½ aguacate,4 lonjas de tocineta,2 lonchas de queso cheddar,Mayonesa al gusto,Sal al gusto",
                    Preparacion = "Abre la pieza de baguette por la mitad y caliéntala en una plancha a fuego suave.Cocina los huevos en omelette de forma tradicional, en una sartén no adherente y Fríe la tocineta en una sartén.",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2021/01/sandwich-de-huevo.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 3,
                    Nombre = "SANDWICH DE SALCHICHON",
                    TipoMercaderiaId = 6,
                    Precio = 3000,
                    Ingredientes = "1 pieza de pan fresco,2 rebanadas de queso blanco,8 lonchas de salchichón,Mayonesa al gusto,Pimienta al gusto",
                    Preparacion = "Coloque el pan en una bandeja para hornear durante 8-10 minutos. Retire el pan del horno y deje que se enfríe. Coloque el queso en rodajas , pepperoni , pimienta y mayonesa .",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2022/11/sandwich-de-salchichon.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 4,
                    Nombre = "BOCATA DE SALMON",
                    TipoMercaderiaId = 6,
                    Precio = 1330,
                    Ingredientes = "300 gr. de salmón noruego ahumado,50 gr. de harina de trigo,Queso crema,Pan de cereales, 2 cucharadas de mayonesa,1/2 cucharadita de eneldo,Ralladura de limón,1/2 limón,Rúcula ",
                    Preparacion = "en un recipiente agregamos la mayonesa, el eneldo, la ralladura de limón, el jugo de limón y mezclamos tostar las rebanadas del pan,le untamos queso crema, luego a la otra rebanada le untamos la mayonesa.Agregamos el salmón, la rúcula y ya.",
                    Imagen = "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/89/IMG_4520_1200px_.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 5,
                    Nombre = "PAPAS ADOBADAS",
                    TipoMercaderiaId = 2,
                    Precio = 500,
                    Ingredientes = "1 kilo de papas cambray,½ cebolla entera,3 dientes de ajo,1 lata de chile chipotle,5 chiles guajillos,Sal al gusto,Pimienta al gusto,½ taza de agua,½ cucharada de achiote",
                    Preparacion = " Asa los chiles guajillo con el ajo y la cebolla entera, hirviendo los chiles por 20 minutos,Licúa todo, menos las papas,Cuela la salsa,Hierve las papas con sal por 10 minutos,Mezcla las papas con el adobo y marina por una hora, hornea por 15",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2020/12/delicious-roasted-young-potatoes-with-dill-top-view-1536x1024.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 6,
                    Nombre = "PASTA ALFREDO",
                    TipoMercaderiaId = 3,
                    Precio = 3100,
                    Ingredientes = "2 porciones de fettuccine también le dicen fettuccini,2 cucharadas de mantequilla,½ taza de queso parmesano rallad,2 cucharadas de mantequilla,1 taza de nata,½ taza de queso parmesano rallado,2 dientes de ajo picados muy pequeño,Pimienta ",
                    Preparacion = "Cocina la pasta, le agregas la mantequilla y el queso .Revuelve, agrega pimienta y listo.Caliente una sartén con aceite y fríe los ajos, agrega la mantequilla y la nata.Agrega el queso parmesano. Mezcla .Ponle la mescla a tu pasta favorita",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2016/06/pasta-5579058_1280.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 7,
                    Nombre = "MINI PIZZAS",
                    TipoMercaderiaId = 5,
                    Precio = 2100,
                    Ingredientes = "400 gramos harina,200 ml agua tibia,2 cucharadas aceite de oliva,15 gramos levadura fresca,pizca de sal,1 taza salsa de jitomate,1 taza queso mozzarella,¼ taza queso parmesano rallado,Pepperoni o jamón al gusto",
                    Preparacion = "En un bowl agrega el aceite, el agua y la levadura.Mezcla y añade la harina ,sal y amasa 5 minutos .reposar tu masa una hora. haz pequeñas tortillitas circulares.Coloca salsa de jitomate, mozzarella ,parmesano y pepperoni .Hornea de15 minutos",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2021/01/pizza-4242967_1280.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 8,
                    Nombre = "SOPA DE PESCADO CON ARROZ",
                    TipoMercaderiaId = 1,
                    Precio = 1900,
                    Ingredientes = "1/2 kg de filetes de pescado blanco merluza o lenguado,1 cebolla,2 dientes de ajo,1 pimiento rojo ,1 papa grande ,1 taza de arroz blanco,perejil fresco ,4 tazas de caldo de pescado,2 cucharadas de aceite de oliva,Sal al gusto,Pimienta negra ",
                    Preparacion = "Calienta olla con aceite . Añade la cebolla, el ajo y el pimiento rojo y cocina por 5 minutos, .Añade las papas, el arroz y cocinar a fuego lento por 15 minutos. luego los filetes de pescado y cocinar 5 minutos .Añade sal y pimienta, y el perejil .",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2023/03/sopa-de-pescado-con-arroz.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 9,
                    Nombre = "MIMOSA",
                    TipoMercaderiaId = 8,
                    Precio = 900,
                    Ingredientes = "½ litro de champaña fría,½ litro de zumo de naranja natural,rodajas de naranja para decorar",
                    Preparacion = "Debes usar una copa larga, sirve la mitad de champaña.Completa la copa con jugo de naranja, es decir, son partes iguales de champaña y de jugo.Decora con frambuesas congeladas y una rodaja de naranja. Disfruta.",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2023/02/Mimosa-paso-3-PG_BCC200120006-300x169.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 10,
                    Nombre = "COCTEL DE CHAMPAÑA, FRESAS Y LIMÓN",
                    TipoMercaderiaId = 8,
                    Precio = 900,
                    Ingredientes = "3 cdas. de puré de fresa fresco,½ cdta. de jugo de limón,Champaña,Fresa cortada en rodajas para decorar,Corteza de limón",
                    Preparacion = "Mezcla el puré de fresas con el limón.En una copa larga añade la mezcla .Rellena el resto de la copa con champaña.Decora con una rebanada de fresa y con corteza de limón. Disfruta.",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2023/02/coctel-con-champagne-paso-4-PG_BCC200120010-300x169.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 11,
                    Nombre = "PALOMA",
                    TipoMercaderiaId = 8,
                    Precio = 900,
                    Ingredientes = "2 onzas de tequila,1 limón,1/2 cda de sal (para escarchar el vaso),1 caso de refresco de toronja,Hielos en cubos",
                    Preparacion = "Toma tu vaso, lo humedecerás col limón en el borde y los vas a escarchar con la sal.Ahora, en la coctelera agregarás el hilo , el tequila, una pizca de sal y un poco de limón,mezclar y servirás en el vaso con la toronja.",
                    Imagen = "https://cdn7.kiwilimon.com/recetaimagen/12746/18280.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 12,
                    Nombre = "SUNRISE",
                    TipoMercaderiaId = 8,
                    Precio = 1000,
                    Ingredientes = "3 onzas de tequila,4 naranjas,3 onzas de granadina,2 rodajas de limón,1 cereza",
                    Preparacion = "En una coctelera, añadirás el hielo, el tequila y el jugo de naranja. Mezcla y añadirás la granadina y un poco de limón.añadir la mezcla del resto de los ingredientes para formar tu cóctel.Decora con las rodajas de limón y la cereza.",
                    Imagen = "https://www.recetasderechupete.com/wp-content/uploads/2022/11/Tequila-Sunrise.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 13,
                    Nombre = "MARTINI DE MAZAPAN",
                    TipoMercaderiaId = 8,
                    Precio = 800,
                    Ingredientes = "1 mazapán,3 hielos,4 oz de leche evaporada,¾ oz de licor de avellana,½ oz de licor de café,1 ½ oz de vodka,Azúcar con canela,Chocolate líquido",
                    Preparacion = "Escarcha la copa con chocolate líquido.Pasa la copa por la mezcla de azúcar con canela.Coloca hielos, la leche , el licor de avellana, 1 pieza de mazapán, licor de café y vodka.Agita.Sirve en la copa y decora con chocolate líquido. ",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2023/04/Martini-de-mazapan-PG_MDM180520002.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 14,
                    Nombre = "CHORIPAN CON CHIMICHURRI",
                    TipoMercaderiaId = 4,
                    Precio = 500,
                    Ingredientes = "1 libra chorizo argentino,2 baguettes,¼ taza agua,6 cdas. vinagre de vino tinto,6 dientes de ajo,1 hoja de laurel,½ cdta. hojuelas de chile seco,1 cda. orégano,1 cda. pimienta,½ taza aceite de oliva,1 taza perejil rizado",
                    Preparacion = "En un bowl agrega el agua, el vinagre, el ajo, el laurel el chile seco,perejil, el orégano, la pimienta y aceite . Mezcla bien.Para el choripán, chorizos en el grill durante 2 minutos.Corta los panes, rellena con el chorizo y chimichurri ",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2022/08/Choripan-con-chimichurri-PG_CHCCH191021_01.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 15,
                    Nombre = "BROCHETAS DE CARNE CON VERDURAS",
                    TipoMercaderiaId = 4,
                    Precio = 700,
                    Ingredientes = "600 gr de carne (cortada para brochetas)1 pimiento rojo,1 cebolla morada,2 calabacitas,Sal, pimienta y ajo en polvo al gusto,Salsa sazonadora o barbecue,2 cdas de cilantro fresco picado.Aceite de oliva",
                    Preparacion = "Añade la carne en un bol, y agrega el jugo de limón, el cilantro picado, el ajo en polvo, la sal y la pimienta. Mezcla .sazonada tu carne,dejarás marinar. coloca un trozo de carne, luego las verduras en el palillo.en sartén con aceite y Cocina",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2022/08/brocheta-de-carne-con-verduras.jpeg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 16,
                    Nombre = "CHUZOS ECUATORIANOS",
                    TipoMercaderiaId = 4,
                    Precio = 800,
                    Ingredientes = "1 kg. lomo de asado limpiozos,3 cdas. aceite con achiote,4 dientes de ajo rallados,1 cda. comino molido,1 cdta. pimienta molida,½ cda. sal,Palitos de madera para chuzos Humedecidos 40 minutos antes de armarlos",
                    Preparacion = "Dentro de un bol añade el ajo, el comino, la sal, el aceite y la pimienta. Mezcla.Agrega la carne y deja marinar por 2 horas.Toma la carne, y ve insertándola en el palito, Sobre la parrilla coloca los chuzos y deja cocinar por 5 minutos ",
                    Imagen = "https://www.comedera.com/wp-content/uploads/2022/08/Chuzos-ecuatorianos-shutterstock_2160614305.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 17,
                    Nombre = "ALITAS DE POLLO ESTILO COREANO",
                    TipoMercaderiaId = 4,
                    Precio = 1200,
                    Ingredientes = "24 alitas de pollo, 150 ml. de salsa de tomate natural,3 cucharadas de salsa de soja,2 cucharadas de vinagre de arroz, 1 cucharada de papelón o panela molida,1 cucharada de sriracha,Semillas de ajonjolí,Sal al gusto,Pimienta al gusto",
                    Preparacion = "En una olla a fuego con tomate natural, la salsa de soja, el vinagre de arroz, el papelón o panela molida, la sriracha y mezcla. agraga un poco de sal y pimienta, y cocinar por 25 minutos.agrega salsa oriental y cocinar por 10 minutos ",
                    Imagen = "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/63/IMG-3268_1200_px__2_.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 18,
                    Nombre = "ARROZ CON LECHE Y CHOCOLATE",
                    TipoMercaderiaId = 10,
                    Precio = 1800,
                    Ingredientes = "100 g. de arroz arborio o arroz bomba,900 ml. de leche entera,1 rama de canela,70 g. de azúcar,1 cucharada de vainilla, 70 g. de chocolate puro,Chocolate rallado a gusto para decorar",
                    Preparacion = "En un cazo, agregamos la leche, la rama de canela, la vainilla y mezclamos, agregamos el arroz y cocinar por 45 minutos .Agregamos el azúcar, removemos y dejamos cocinar por 10 minutos, agregamos el chocolate fundido a nuestro arroz con leche y mescla.",
                    Imagen = "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/491/arroz_con_leche_y_chocolate.webp"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 19,
                    Nombre = "CASTAÑAS DE RICOTA",
                    TipoMercaderiaId = 10,
                    Precio = 900,
                    Ingredientes = "250 g. de ricota,150 g. de harina,60 g. de azúcar,1 huevo,Ralladura de limón,6 g. de polvo de hornear,Azúcar Glass,Leche condensada,Aceite para freír",
                    Preparacion = "En un recipiente colocar la ricota, el huevo, el azúcar, y mezclamos todo, agregamos la ralladura de limón, la harina y mezclamos . formar las bolitas, las freímos hasta dorar, las sacamos y Econ el agregamos azúcar glass y leche condensada ",
                    Imagen = "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/191/IMG_8913-1200.jpg"
                });
                entity.HasData(new Mercaderia
                {
                    MercaderiaId = 20,
                    Nombre = "BIZCOCHO DE PLÁTANO Y CHOCOLATE",
                    TipoMercaderiaId = 10,
                    Precio = 850,
                    Ingredientes = "2 plátanos cortados en rodajas,2 huevos,150 g. de azúcar,225 g. de harina de trigo todo uso,1 y 1/2 cucharadita de levadura química o polvo de hornear,80 g. de chocolate,1 cucharadita de canela en polvo,100 ml. de aceite vegetal",
                    Preparacion = "trituramos nuestro plátano y en un recipiente colocar el aceite, el azúcar y con la ayuda de unas varillas, mezclar, agregamos los huevos y el plátano mezclar.agregamos la levadura, la canela en polvo, el chocolate  y horno por 45 minutos",
                    Imagen = "https://elmundoenrecetas.s3.amazonaws.com/uploads/recipe/main_image/261/IMG_1675-1280.jpg"
                });
            });

            modelBuilder.Entity<TipoMercaderia>(entity =>
            {
                entity.HasKey(k => k.TipoMercaderiaId);
                entity.Property(t => t.TipoMercaderiaId).ValueGeneratedOnAdd();
                entity.HasMany<Mercaderia>(m => m.LsMercaderia).WithOne(o => o.FKTipoMercaderia);
                entity.Property(s => s.Descripcion).IsRequired().HasMaxLength(50);
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "ENTRADA"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 2,
                    Descripcion = "MINUTAS"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 3,
                    Descripcion = "PASTAS"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 4,
                    Descripcion = "PARRILLA"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 5,
                    Descripcion = "Pizzas"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 6,
                    Descripcion = "SANDWICH"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 7,
                    Descripcion = "ENSALADAS"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 8,
                    Descripcion = "BEBIDAS"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 9,
                    Descripcion = "CERVEZA ARTESANAL"
                });
                entity.HasData(new TipoMercaderia
                {
                    TipoMercaderiaId = 10,
                    Descripcion = "POSTRES"
                });

            });
        }
    }
}

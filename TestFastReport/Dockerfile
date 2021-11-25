FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
RUN apt-get update && apt-get install -y --allow-unauthenticated libc6-dev libgdiplus libx11-dev
RUN apt-get install -y libgif-dev autoconf libtool automake build-essential gettext libglib2.0-dev libcairo2-dev libtiff-dev libexif-dev libpango1.0-dev git
WORKDIR /var/customLib
RUN git clone https://github.com/mono/libgdiplus.git
WORKDIR /var/customLib/libgdiplus
RUN ./autogen.sh --with-pango --prefix=/usr
RUN make
RUN make install
COPY ./ /app/
WORKDIR /app
EXPOSE 80
ENTRYPOINT ["dotnet", "TestFastReport.dll"]
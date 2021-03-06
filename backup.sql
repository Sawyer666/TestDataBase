PGDMP     %    
                x         	   MessageDb    12.3    12.3                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16466 	   MessageDb    DATABASE     �   CREATE DATABASE "MessageDb" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';
    DROP DATABASE "MessageDb";
                postgres    false            �            1255    16562    add_to_log()    FUNCTION     �   CREATE FUNCTION public.add_to_log() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO LogMessages(MesasgeId,CurrentTime)VALUES(New.Id,NOW());
RETURN NEW;
END;
    $$;
 #   DROP FUNCTION public.add_to_log();
       public          postgres    false            �            1255    16638    update_to_log()    FUNCTION     �   CREATE FUNCTION public.update_to_log() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN	
	UPDATE LogMessages SET CurrentTime = NOW() WHERE MesasgeId = New.Id;
RETURN NEW;
END;  
   $$;
 &   DROP FUNCTION public.update_to_log();
       public          postgres    false            �            1259    16627    logmessages    TABLE     �   CREATE TABLE public.logmessages (
    id integer NOT NULL,
    mesasgeid integer NOT NULL,
    currenttime timestamp without time zone
);
    DROP TABLE public.logmessages;
       public         heap    postgres    false            �            1259    16625    logmessages_id_seq    SEQUENCE     �   CREATE SEQUENCE public.logmessages_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.logmessages_id_seq;
       public          postgres    false    205                       0    0    logmessages_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.logmessages_id_seq OWNED BY public.logmessages.id;
          public          postgres    false    204            �            1259    16526    messages    TABLE     L   CREATE TABLE public.messages (
    id integer NOT NULL,
    message text
);
    DROP TABLE public.messages;
       public         heap    postgres    false            �            1259    16524    messages_id_seq    SEQUENCE     �   CREATE SEQUENCE public.messages_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.messages_id_seq;
       public          postgres    false    203                       0    0    messages_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.messages_id_seq OWNED BY public.messages.id;
          public          postgres    false    202            �
           2604    16630    logmessages id    DEFAULT     p   ALTER TABLE ONLY public.logmessages ALTER COLUMN id SET DEFAULT nextval('public.logmessages_id_seq'::regclass);
 =   ALTER TABLE public.logmessages ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    204    205    205            �
           2604    16529    messages id    DEFAULT     j   ALTER TABLE ONLY public.messages ALTER COLUMN id SET DEFAULT nextval('public.messages_id_seq'::regclass);
 :   ALTER TABLE public.messages ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    202    203    203                      0    16627    logmessages 
   TABLE DATA           A   COPY public.logmessages (id, mesasgeid, currenttime) FROM stdin;
    public          postgres    false    205                    0    16526    messages 
   TABLE DATA           /   COPY public.messages (id, message) FROM stdin;
    public          postgres    false    203   b                  0    0    logmessages_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.logmessages_id_seq', 60, true);
          public          postgres    false    204                       0    0    messages_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.messages_id_seq', 83, true);
          public          postgres    false    202            �
           2606    16632    logmessages logmessages_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.logmessages
    ADD CONSTRAINT logmessages_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.logmessages DROP CONSTRAINT logmessages_pkey;
       public            postgres    false    205            �
           2606    16534    messages messages_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.messages DROP CONSTRAINT messages_pkey;
       public            postgres    false    203            �
           2620    16648    messages add_log_trigger    TRIGGER     r   CREATE TRIGGER add_log_trigger AFTER INSERT ON public.messages FOR EACH ROW EXECUTE FUNCTION public.add_to_log();
 1   DROP TRIGGER add_log_trigger ON public.messages;
       public          postgres    false    203    206            �
           2620    16649    messages update_log_trigger    TRIGGER     x   CREATE TRIGGER update_log_trigger AFTER UPDATE ON public.messages FOR EACH ROW EXECUTE FUNCTION public.update_to_log();
 4   DROP TRIGGER update_log_trigger ON public.messages;
       public          postgres    false    203    207            �
           2606    16633 &   logmessages logmessages_mesasgeid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.logmessages
    ADD CONSTRAINT logmessages_mesasgeid_fkey FOREIGN KEY (mesasgeid) REFERENCES public.messages(id) ON DELETE CASCADE;
 P   ALTER TABLE ONLY public.logmessages DROP CONSTRAINT logmessages_mesasgeid_fkey;
       public          postgres    false    203    205    2699               E   x�e�A�0�7���0�DK�먀�{�eB��Y��y��i=��� �w�6�J92~�7̓۩�����            x��0�4�0�4�.CNC0������ \�C     